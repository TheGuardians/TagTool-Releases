﻿using System.Collections.Generic;
using System.IO;
using TagTool.Tags.Definitions;
using TagTool.Cache;
using TagTool.IO;
using TagTool.Tags.Resources;
using TagTool.Serialization;
using TagTool.Geometry;

namespace TagTool.Commands.ScenarioStructureBSPs
{
    class CollisionTestCommand : Command
    {
        private HaloOnlineCacheContext CacheContext { get; }
        private CachedTagInstance Tag { get; }
        private ScenarioStructureBsp BSP { get; }

        public CollisionTestCommand(HaloOnlineCacheContext cacheContext, CachedTagInstance tag, ScenarioStructureBsp bsp)
            : base(true,

                  "CollisionTest",
                  "A test resource-loading command for 'sbsp' tag collision.",
                  
                  "collision-test",

                  "A test resource-loading command for 'sbsp' tag collision.")
        {
            CacheContext = cacheContext;
            Tag = tag;
            BSP = bsp;
        }

        public override object Execute(List<string> args)
        {
            // Deserialize the definition data
            var resourceContext = new ResourceSerializationContext(CacheContext, BSP.CollisionBspResource);
            var definition = CacheContext.Deserializer.Deserialize<StructureBspTagResources>(resourceContext);

            // Extract the resource data
            var resourceDataStream = new MemoryStream();
            CacheContext.ExtractResource(BSP.CollisionBspResource, resourceDataStream);

            using (var reader = new EndianReader(resourceDataStream))
            {
                #region collision bsps

                foreach (var cbsp in definition.CollisionBsps)
                {
                    reader.BaseStream.Position = cbsp.Bsp3dNodes.Address.Offset;
                    for (var i = 0; i < cbsp.Bsp3dNodes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp3dNode));
                        cbsp.Bsp3dNodes.Add((CollisionGeometry.Bsp3dNode)element);
                    }

                    reader.BaseStream.Position = cbsp.Planes.Address.Offset;
                    for (var i = 0; i < cbsp.Planes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Plane));
                        cbsp.Planes.Add((CollisionGeometry.Plane)element);
                    }

                    reader.BaseStream.Position = cbsp.Leaves.Address.Offset;
                    for (var i = 0; i < cbsp.Leaves.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Leaf));
                        cbsp.Leaves.Add((CollisionGeometry.Leaf)element);
                    }

                    reader.BaseStream.Position = cbsp.Bsp2dReferences.Address.Offset;
                    for (var i = 0; i < cbsp.Bsp2dReferences.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dReference));
                        cbsp.Bsp2dReferences.Add((CollisionGeometry.Bsp2dReference)element);
                    }

                    reader.BaseStream.Position = cbsp.Bsp2dNodes.Address.Offset;
                    for (var i = 0; i < cbsp.Bsp2dNodes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dNode));
                        cbsp.Bsp2dNodes.Add((CollisionGeometry.Bsp2dNode)element);
                    }

                    reader.BaseStream.Position = cbsp.Surfaces.Address.Offset;
                    for (var i = 0; i < cbsp.Surfaces.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Surface));
                        cbsp.Surfaces.Add((CollisionGeometry.Surface)element);
                    }

                    reader.BaseStream.Position = cbsp.Edges.Address.Offset;
                    for (var i = 0; i < cbsp.Edges.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Edge));
                        cbsp.Edges.Add((CollisionGeometry.Edge)element);
                    }

                    reader.BaseStream.Position = cbsp.Vertices.Address.Offset;
                    for (var i = 0; i < cbsp.Vertices.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Vertex));
                        cbsp.Vertices.Add((CollisionGeometry.Vertex)element);
                    }
                }

                #endregion

                #region large collision bsps

                foreach (var cbsp in definition.LargeCollisionBsps)
                {
                    reader.BaseStream.Position = cbsp.Bsp3dNodes.Address.Offset;
                    for (var i = 0; i < cbsp.Bsp3dNodes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp3dNode));
                        cbsp.Bsp3dNodes.Add((CollisionGeometry.Bsp3dNode)element);
                    }

                    reader.BaseStream.Position = cbsp.Planes.Address.Offset;
                    for (var i = 0; i < cbsp.Planes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Plane));
                        cbsp.Planes.Add((CollisionGeometry.Plane)element);
                    }

                    reader.BaseStream.Position = cbsp.Leaves.Address.Offset;
                    for (var i = 0; i < cbsp.Leaves.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Leaf));
                        cbsp.Leaves.Add((CollisionGeometry.Leaf)element);
                    }

                    reader.BaseStream.Position = cbsp.Bsp2dReferences.Address.Offset;
                    for (var i = 0; i < cbsp.Bsp2dReferences.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dReference));
                        cbsp.Bsp2dReferences.Add((CollisionGeometry.Bsp2dReference)element);
                    }

                    reader.BaseStream.Position = cbsp.Bsp2dNodes.Address.Offset;
                    for (var i = 0; i < cbsp.Bsp2dNodes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dNode));
                        cbsp.Bsp2dNodes.Add((CollisionGeometry.Bsp2dNode)element);
                    }

                    reader.BaseStream.Position = cbsp.Surfaces.Address.Offset;
                    for (var i = 0; i < cbsp.Surfaces.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Surface));
                        cbsp.Surfaces.Add((CollisionGeometry.Surface)element);
                    }

                    reader.BaseStream.Position = cbsp.Edges.Address.Offset;
                    for (var i = 0; i < cbsp.Edges.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Edge));
                        cbsp.Edges.Add((CollisionGeometry.Edge)element);
                    }

                    reader.BaseStream.Position = cbsp.Vertices.Address.Offset;
                    for (var i = 0; i < cbsp.Vertices.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Vertex));
                        cbsp.Vertices.Add((CollisionGeometry.Vertex)element);
                    }
                }

                #endregion

                #region compressions

                foreach (var instance in definition.InstancedGeometry)
                {
                    #region compression's resource data

                    reader.BaseStream.Position = instance.CollisionBsp.Bsp3dNodes.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Bsp3dNodes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp3dNode));
                        instance.CollisionBsp.Bsp3dNodes.Add((CollisionGeometry.Bsp3dNode)element);
                    }

                    reader.BaseStream.Position = instance.CollisionBsp.Planes.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Planes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Plane));
                        instance.CollisionBsp.Planes.Add((CollisionGeometry.Plane)element);
                    }

                    reader.BaseStream.Position = instance.CollisionBsp.Leaves.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Leaves.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Leaf));
                        instance.CollisionBsp.Leaves.Add((CollisionGeometry.Leaf)element);
                    }

                    reader.BaseStream.Position = instance.CollisionBsp.Bsp2dReferences.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Bsp2dReferences.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dReference));
                        instance.CollisionBsp.Bsp2dReferences.Add((CollisionGeometry.Bsp2dReference)element);
                    }

                    reader.BaseStream.Position = instance.CollisionBsp.Bsp2dNodes.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Bsp2dNodes.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dNode));
                        instance.CollisionBsp.Bsp2dNodes.Add((CollisionGeometry.Bsp2dNode)element);
                    }

                    reader.BaseStream.Position = instance.CollisionBsp.Surfaces.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Surfaces.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Surface));
                        instance.CollisionBsp.Surfaces.Add((CollisionGeometry.Surface)element);
                    }

                    reader.BaseStream.Position = instance.CollisionBsp.Edges.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Edges.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Edge));
                        instance.CollisionBsp.Edges.Add((CollisionGeometry.Edge)element);
                    }

                    reader.BaseStream.Position = instance.CollisionBsp.Vertices.Address.Offset;
                    for (var i = 0; i < instance.CollisionBsp.Vertices.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Vertex));
                        instance.CollisionBsp.Vertices.Add((CollisionGeometry.Vertex)element);
                    }

                    #endregion

                    #region compression's other resource data

                    foreach (var cbsp in instance.CollisionGeometries)
                    {
                        reader.BaseStream.Position = cbsp.Bsp3dNodes.Address.Offset;
                        for (var i = 0; i < cbsp.Bsp3dNodes.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp3dNode));
                            cbsp.Bsp3dNodes.Add((CollisionGeometry.Bsp3dNode)element);
                        }

                        reader.BaseStream.Position = cbsp.Planes.Address.Offset;
                        for (var i = 0; i < cbsp.Planes.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Plane));
                            cbsp.Planes.Add((CollisionGeometry.Plane)element);
                        }

                        reader.BaseStream.Position = cbsp.Leaves.Address.Offset;
                        for (var i = 0; i < cbsp.Leaves.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Leaf));
                            cbsp.Leaves.Add((CollisionGeometry.Leaf)element);
                        }

                        reader.BaseStream.Position = cbsp.Bsp2dReferences.Address.Offset;
                        for (var i = 0; i < cbsp.Bsp2dReferences.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dReference));
                            cbsp.Bsp2dReferences.Add((CollisionGeometry.Bsp2dReference)element);
                        }

                        reader.BaseStream.Position = cbsp.Bsp2dNodes.Address.Offset;
                        for (var i = 0; i < cbsp.Bsp2dNodes.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Bsp2dNode));
                            cbsp.Bsp2dNodes.Add((CollisionGeometry.Bsp2dNode)element);
                        }

                        reader.BaseStream.Position = cbsp.Surfaces.Address.Offset;
                        for (var i = 0; i < cbsp.Surfaces.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Surface));
                            cbsp.Surfaces.Add((CollisionGeometry.Surface)element);
                        }

                        reader.BaseStream.Position = cbsp.Edges.Address.Offset;
                        for (var i = 0; i < cbsp.Edges.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Edge));
                            cbsp.Edges.Add((CollisionGeometry.Edge)element);
                        }

                        reader.BaseStream.Position = cbsp.Vertices.Address.Offset;
                        for (var i = 0; i < cbsp.Vertices.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(CollisionGeometry.Vertex));
                            cbsp.Vertices.Add((CollisionGeometry.Vertex)element);
                        }
                    }

                    #endregion

                    #region Unknown Data

                    for (var i = 0; i < instance.Unknown1.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(StructureBspTagResources.InstancedGeometryBlock.Unknown1Block));
                        instance.Unknown1.Add((StructureBspTagResources.InstancedGeometryBlock.Unknown1Block)element);
                    }

                    for (var i = 0; i < instance.Unknown2.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(StructureBspTagResources.InstancedGeometryBlock.Unknown2Block));
                        instance.Unknown2.Add((StructureBspTagResources.InstancedGeometryBlock.Unknown2Block)element);
                    }

                    for (var i = 0; i < instance.Unknown3.Count; i++)
                    {
                        var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(StructureBspTagResources.InstancedGeometryBlock.Unknown3Block));
                        instance.Unknown3.Add((StructureBspTagResources.InstancedGeometryBlock.Unknown3Block)element);
                    }

                    #endregion

                    #region compression's havok collision data

                    foreach (var collision in instance.CollisionMoppCodes)
                    {
                        for (var i = 0; i < collision.Data.Count; i++)
                        {
                            var element = CacheContext.Deserializer.DeserializeValue(reader, null, null, typeof(byte));
                            collision.Data.Add(new StructureBspTagResources.CollisionMoppCodeResource.Datum { Value = (byte)element });
                        }
                    }

                    #endregion
                }

                #endregion
            }

            return true;
        }
    }
}