using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagTool.Cache;
using TagTool.Cache.HaloOnline;
using TagTool.Commands.Common;
using TagTool.Common;
using TagTool.Scripting;
using TagTool.Tags;
using TagTool.Tags.Definitions;

namespace TagTool.Commands.Porting
{
    public class PortTagHOCommand : Command
    {
        [Flags]
        public enum PortingFlags
        {
            None,

            Replace = 1 << 0,
            Recursive = 1 << 1,
            Single = 1 << 2,

            Default = Recursive
        }

        private readonly GameCacheHaloOnlineBase SrcCache;
        private Stream SrcStream;
        private GameCacheHaloOnlineBase DestCache;
        private Stream DestStream;
        private PortingFlags Flags = PortingFlags.Default;

        private Dictionary<int, CachedTag> ConvertedTags = new Dictionary<int, CachedTag>();
        private Dictionary<ResourceLocation, Dictionary<int, PageableResource>> ConvertedResources = new Dictionary<ResourceLocation, Dictionary<int, PageableResource>>();

        public PortTagHOCommand(GameCacheHaloOnlineBase destCache, GameCacheHaloOnlineBase srcCache) : base(false, "PortTag", "", "", "")
        {
            SrcCache = srcCache;
            DestCache = destCache;

            foreach (ResourceLocation location in Enum.GetValues(typeof(ResourceLocation)))
                ConvertedResources[location] = new Dictionary<int, PageableResource>();
        }

        public override object Execute(List<string> args)
        {
            if (args.Count < 1)
                return new TagToolError(CommandError.ArgCount);

            if (!ParsePortingFlags(args, out Flags))
                return false;

            try
            {
                using (DestStream = DestCache.OpenCacheReadWrite())
                using (SrcStream = SrcCache.OpenCacheRead())
                {
                    foreach (var srcTag in ParseLegacyTag(args.Last()))
                        ConvertTag(srcTag);
                }
            }
            finally
            {
                DestCache.SaveStrings();
                DestCache.SaveTagNames();
            }

            return true;
        }

        private bool ParsePortingFlags(List<string> args, out PortingFlags outFlags)
        {
            outFlags = PortingFlags.Default;

            for (int i = 0; i < args.Count - 1; i++)
            {
                string arg = args[i];
                bool not = arg.Length > 0 && arg[0] == '!';
                if (not)
                    arg = arg.Substring(1);


                if (!Enum.TryParse(arg, true, out PortingFlags flags))
                {
                    new TagToolError(CommandError.ArgInvalid, $"Unknown porting flag '{arg}'");
                    return false;
                }

                if (not)
                    outFlags &= ~flags;
                else
                    outFlags |= flags;
            }

            if (outFlags.HasFlag(PortingFlags.Single)) outFlags &= ~PortingFlags.Recursive;

            return true;
        }

        private object ConvertData(object tagData)
        {
            switch (tagData)
            {
                case PageableResource pageable:
                    return ConvertResource(pageable);
                case byte[] _:
                    return tagData;
                case IList list:
                    return ConvertList(list);
                case Scenario scnr:
                    return ConvertScenario(scnr);
                case TagStructure tagStructure:
                    return CopyStructure(tagStructure);
                case CachedTag tagReference:
                    return ConvertTag(tagReference, true);
                default:
                    return tagData;
            }
        }

        private Scenario ConvertScenario(Scenario scnr)
        {
            scnr = (Scenario)CopyStructure(scnr);

            // Fixup script tag refs
            foreach (HsSyntaxNode node in scnr.ScriptExpressions)
            {
                if (node.Flags.HasFlag(HsSyntaxNodeFlags.Primitive)
                    && node.ValueType.HaloOnline >= HsType.HaloOnlineValue.Sound
                    && node.ValueType.HaloOnline <= HsType.HaloOnlineValue.AnyTagNotResolving)
                {
                  
                    int srcTagIndex = BitConverter.ToInt32(node.Data, 0);
                    var srcTag = SrcCache.TagCacheGenHO.Tags[srcTagIndex];

                    if(!DestCache.TagCacheGenHO.TryGetTag(srcTag.ToString(), out CachedTag destTag))
                    {
                        new TagToolWarning($"Missing tag in scenario script '{srcTag}'");
                        int destTagIndex = -1;
                        node.Data = BitConverter.GetBytes(destTagIndex);
                    }
                    else
                    {
                        node.Data = BitConverter.GetBytes(destTag.Index);
                    }
                }
            }

            return scnr;
        }

        private object ConvertResource(PageableResource pageable)
        {
            if (pageable == null || pageable.Page.Index < 0 || pageable.Page.CompressedBlockSize == 0)
                return pageable;

            ResourceLocation location = pageable.GetLocation();
            if (location == ResourceLocation.Mods)
            {
                pageable.ChangeLocation(ResourceLocation.ResourcesB);
            }

            if (ConvertedResources[location].ContainsKey(pageable.Page.Index))
                return ConvertedResources[location][pageable.Page.Index];

            ConvertedResources[location][pageable.Page.Index] = pageable;
            byte[] data = SrcCache.ResourceCaches.ExtractRawResource(pageable);
            DestCache.ResourceCaches.AddRawResource(pageable, data);
            return pageable;
        }

        private IList ConvertList(IList collection)
        {
            for (int i = 0; i < collection.Count; i++)
                collection[i] = ConvertData(collection[i]);
            return collection;
        }

        private TagStructure CopyStructure(TagStructure structure)
        {
            foreach (TagFieldInfo field in structure.GetTagFieldEnumerable(SrcCache.Version, SrcCache.Platform))
                field.SetValue(structure, ConvertData(field.GetValue(structure)));
            return structure;
        }

        public CachedTag ConvertTag(CachedTag srcTag, bool isTagRef = false)
        {
            if (ConvertedTags.ContainsKey(srcTag.Index))
                return ConvertedTags[srcTag.Index];

            DestCache.TagCache.TryGetTag(srcTag.ToString(), out CachedTag existingTag);

            if (isTagRef && !Flags.HasFlag(PortingFlags.Recursive))
                return existingTag;
            if (existingTag != null && !ShouldReplaceTag(srcTag))
                return existingTag;

            LogInfo($"Converting {srcTag}...");

            CachedTag destTag = existingTag != null
                ? existingTag
                : DestCache.TagCacheGenHO.AllocateTag(srcTag.Group, srcTag.Name);

            ConvertedTags[srcTag.Index] = destTag;

            object tagData = SrcCache.Deserialize(SrcStream, srcTag);
            tagData = ConvertData(tagData);
            DestCache.Serialize(DestStream, destTag, tagData);

            return destTag;
        }

        private bool ShouldReplaceTag(CachedTag srcTag)
        {
            return Flags.HasFlag(PortingFlags.Replace) && 
                !srcTag.IsInGroup(DoNotReplaceGroupsCommand.UserDefinedDoNotReplaceGroups.Cast<Tag>().ToArray());
        }

        private void LogInfo(string message) => Console.WriteLine(message);

        private List<CachedTag> ParseLegacyTag(string tagSpecifier)
        {
            List<CachedTag> result = new List<CachedTag>();

            if (tagSpecifier.Length == 0 || (!char.IsLetter(tagSpecifier[0]) && !tagSpecifier.Contains('*')) || !tagSpecifier.Contains('.'))
            {
                new TagToolError(CommandError.CustomError, $"Invalid tag name: {tagSpecifier}");
                return new List<CachedTag>();
            }

            var tagIdentifiers = tagSpecifier.Split('.');

            if (!DestCache.TagCache.TryParseGroupTag(tagIdentifiers[1], out var groupTag))
            {
                new TagToolError(CommandError.CustomError, $"Invalid tag name: {tagSpecifier}");
                return new List<CachedTag>();
            }

            var tagName = tagIdentifiers[0];

            // find the CacheFile.IndexItem(s)
            if (tagName == "*") result = SrcCache.TagCache.TagTable.ToList().FindAll(
                item => item != null && item.IsInGroup(groupTag));
            else result.Add(SrcCache.TagCache.TagTable.ToList().Find(
                item => item != null && item.IsInGroup(groupTag) && tagName == item.Name));

            if (result.Count == 0)
            {
                new TagToolError(CommandError.CustomError, $"Invalid tag name: {tagSpecifier}");
                return new List<CachedTag>();
            }

            return result;
        }
    }
}
