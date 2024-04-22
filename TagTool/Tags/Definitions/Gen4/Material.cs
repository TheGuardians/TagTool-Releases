using TagTool.Cache;
using TagTool.Common;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static TagTool.Tags.TagFieldFlags;

namespace TagTool.Tags.Definitions.Gen4
{
    [TagStructure(Name = "material", Tag = "mat ", Size = 0x44)]
    public class Material : TagStructure
    {
        [TagField(ValidTags = new [] { "mats" })]
        public CachedTag MaterialShader;
        public List<MaterialShaderParameterBlock> MaterialParameters;
        public List<MaterialPostprocessBlock> PostprocessDefinition;
        public StringId PhysicsMaterialName;
        public StringId PhysicsMaterialName2;
        public StringId PhysicsMaterialName3;
        public StringId PhysicsMaterialName4;
        public float SortOffset;
        public AlphaBlendModeEnum AlphaBlendMode;
        public GlobalSortLayerEnumDefintion SortLayer;
        public Materialflags Flags;
        public MaterialrenderFlags RenderFlags;
        public MaterialTransparentShadowPolicyEnum TransparentShadowPolicy;
        [TagField(Length = 0x3, Flags = TagFieldFlags.Padding)]
        public byte[] Padding;
        
        public enum AlphaBlendModeEnum : sbyte
        {
            Opaque,
            Additive,
            Multiply,
            AlphaBlend,
            DoubleMultiply,
            PreMultipliedAlpha,
            Maximum,
            MultiplyAdd,
            AddSrcTimesDstalpha,
            AddSrcTimesSrcalpha,
            InvAlphaBlend,
            MotionBlurStatic,
            MotionBlurInhibit,
            ApplyShadowIntoShadowMask,
            AlphaBlendConstant,
            OverdrawApply,
            WetScreenEffect,
            Minimum,
            Revsubtract,
            ForgeLightmap,
            ForgeLightmapInv,
            ReplaceAllChannels,
            AlphaBlendMax,
            OpaqueAlphaBlend,
            AlphaBlendAdditiveTransparent
        }
        
        public enum GlobalSortLayerEnumDefintion : sbyte
        {
            Invalid,
            PrePass,
            Normal,
            PostPass
        }
        
        [Flags]
        public enum Materialflags : byte
        {
            ConvertedFromShader = 1 << 0,
            DecalPostLighting = 1 << 1
        }
        
        [Flags]
        public enum MaterialrenderFlags : byte
        {
            ResolveScreenBeforeRendering = 1 << 0
        }
        
        public enum MaterialTransparentShadowPolicyEnum : sbyte
        {
            None,
            RenderAsDecal,
            RenderWithMaterial
        }
        
        [TagStructure(Size = 0xA8)]
        public class MaterialShaderParameterBlock : TagStructure
        {
            public StringId ParameterName;
            public MaterialShaderParameterTypeEnum ParameterType;
            public int ParameterIndex;
            [TagField(ValidTags = new [] { "bitm" })]
            public CachedTag Bitmap;
            public StringId BitmapPath;
            public RealArgbColor Color;
            public float Real;
            public RealVector3d Vector;
            public int IntBool;
            public ushort BitmapFlags;
            public ushort BitmapFilterMode;
            public ushort BitmapAddressMode;
            public ushort BitmapAddressModeX;
            public ushort BitmapAddressModeY;
            public ushort BitmapSharpenMode;
            public byte BitmapExternMode;
            public byte BitmapMinMipmap;
            public byte BitmapMaxMipmap;
            public byte RenderPhasesUsed;
            public List<MaterialShaderFunctionParameterBlock> FunctionParameters;
            public byte[] DisplayName;
            public byte[] DisplayGroup;
            public byte[] DisplayHelpText;
            public float DisplayMinimum;
            public float DisplayMaximum;
            public byte RegisterIndex;
            public byte RegisterOffset;
            public byte RegisterCount;
            public RegisterSetEnum RegisterSet;
            
            public enum MaterialShaderParameterTypeEnum : int
            {
                Bitmap,
                Real,
                Int,
                Bool,
                Color
            }
            
            public enum RegisterSetEnum : sbyte
            {
                Bool,
                Int,
                Float,
                Sampler,
                VertexBool,
                VertexInt,
                VertexFloat,
                VertexSampler
            }
            
            [TagStructure(Size = 0x2C)]
            public class MaterialShaderFunctionParameterBlock : TagStructure
            {
                public MaterialAnimatedParameterTypeEnum Type;
                public StringId InputName;
                public StringId RangeName;
                public MaterialfunctionOutputModEnum OutputModifier;
                [TagField(Length = 0x3, Flags = TagFieldFlags.Padding)]
                public byte[] Padding;
                public StringId OutputModifierInput;
                public float TimePeriod; // seconds
                public MappingFunction Function;
                
                public enum MaterialAnimatedParameterTypeEnum : int
                {
                    Value,
                    Color,
                    ScaleUniform,
                    ScaleU,
                    ScaleV,
                    OffsetU,
                    OffsetV,
                    FrameIndex,
                    Alpha
                }
                
                public enum MaterialfunctionOutputModEnum : sbyte
                {
                    Unknown,
                    Add,
                    Multiply
                }
                
                [TagStructure(Size = 0x14)]
                public class MappingFunction : TagStructure
                {
                    public byte[] Data;
                }
            }
        }
        
        [TagStructure(Size = 0x9C)]
        public class MaterialPostprocessBlock : TagStructure
        {
            public List<MaterialPostprocessTextureBlock> Textures;
            public List<RealVector4dBlock> TextureConstants;
            public List<RealVector4dBlock> RealVectors;
            public List<RealVector4dBlock> RealVertexVectors;
            public List<IntBlock> IntConstants;
            public int BoolConstants;
            public int UsedBoolConstants;
            public int BoolRenderPhaseMask;
            public int VertexBoolConstants;
            public int UsedVertexBoolConstants;
            public int VertexBoolRenderPhaseMask;
            public List<MaterialShaderFunctionParameterBlock> Functions;
            public List<FunctionparameterBlock> FunctionParameters;
            public List<ExternparameterBlock> ExternParameters;
            public AlphaBlendModeEnum AlphaBlendMode;
            public LayerblendModeEnum LayerBlendMode;
            public MaterialpostprocessFlags Flags;
            [TagField(Length = 12)]
            public RuntimeQueryableProperties[]  RuntimeQueryablePropertiesTable;
            public MaterialTypeStruct PhysicsMaterial0;
            public MaterialTypeStruct PhysicsMaterial1;
            public MaterialTypeStruct PhysicsMaterial2;
            public MaterialTypeStruct PhysicsMaterial3;
            
            public enum AlphaBlendModeEnum : sbyte
            {
                Opaque,
                Additive,
                Multiply,
                AlphaBlend,
                DoubleMultiply,
                PreMultipliedAlpha,
                Maximum,
                MultiplyAdd,
                AddSrcTimesDstalpha,
                AddSrcTimesSrcalpha,
                InvAlphaBlend,
                MotionBlurStatic,
                MotionBlurInhibit,
                ApplyShadowIntoShadowMask,
                AlphaBlendConstant,
                OverdrawApply,
                WetScreenEffect,
                Minimum,
                Revsubtract,
                ForgeLightmap,
                ForgeLightmapInv,
                ReplaceAllChannels,
                AlphaBlendMax,
                OpaqueAlphaBlend,
                AlphaBlendAdditiveTransparent
            }
            
            public enum LayerblendModeEnum : sbyte
            {
                None,
                Blended,
                Layered
            }
            
            [Flags]
            public enum MaterialpostprocessFlags : ushort
            {
                WireframeOutline = 1 << 0,
                ForceSinglePass = 1 << 1,
                HasPixelConstantFunctions = 1 << 2,
                HasVertexConstantFunctions = 1 << 3,
                HasTextureTransformFunctions = 1 << 4,
                HasTextureFrameFunctions = 1 << 5,
                ResolveScreenBeforeRendering = 1 << 6,
                DisableAtmosphereFog = 1 << 7,
                UsesDepthCamera = 1 << 8,
                MaterialIsVariable = 1 << 9
            }
            
            [TagStructure(Size = 0x18)]
            public class MaterialPostprocessTextureBlock : TagStructure
            {
                [TagField(ValidTags = new [] { "bitm" })]
                public CachedTag BitmapReference;
                public byte AddressMode;
                public byte FilterMode;
                public byte FrameIndexParameter;
                public byte SamplerIndex;
                public sbyte LevelOfSmallestMipmapToUse;
                public sbyte LevelOfLargestMipmapToUse;
                public byte RenderPhaseMask;
                [TagField(Length = 0x1, Flags = TagFieldFlags.Padding)]
                public byte[] Padding;
            }
            
            [TagStructure(Size = 0x10)]
            public class RealVector4dBlock : TagStructure
            {
                public RealVector3d Vector;
                public float VectorW;
            }
            
            [TagStructure(Size = 0x4)]
            public class IntBlock : TagStructure
            {
                public int IntValue;
            }
            
            [TagStructure(Size = 0x2C)]
            public class MaterialShaderFunctionParameterBlock : TagStructure
            {
                public MaterialAnimatedParameterTypeEnum Type;
                public StringId InputName;
                public StringId RangeName;
                public MaterialfunctionOutputModEnum OutputModifier;
                [TagField(Length = 0x3, Flags = TagFieldFlags.Padding)]
                public byte[] Padding;
                public StringId OutputModifierInput;
                public float TimePeriod; // seconds
                public MappingFunction Function;
                
                public enum MaterialAnimatedParameterTypeEnum : int
                {
                    Value,
                    Color,
                    ScaleUniform,
                    ScaleU,
                    ScaleV,
                    OffsetU,
                    OffsetV,
                    FrameIndex,
                    Alpha
                }
                
                public enum MaterialfunctionOutputModEnum : sbyte
                {
                    Unknown,
                    Add,
                    Multiply
                }
                
                [TagStructure(Size = 0x14)]
                public class MappingFunction : TagStructure
                {
                    public byte[] Data;
                }
            }
            
            [TagStructure(Size = 0x4)]
            public class FunctionparameterBlock : TagStructure
            {
                public byte FunctionIndex;
                public byte RenderPhaseMask;
                public byte RegisterIndex;
                public byte RegisterOffset;
            }
            
            [TagStructure(Size = 0x4)]
            public class ExternparameterBlock : TagStructure
            {
                public byte ExternIndex;
                public byte ExternRegister;
                public byte ExternOffset;
                public byte RenderPhaseMask;
            }
            
            [TagStructure(Size = 0x2)]
            public class RuntimeQueryableProperties : TagStructure
            {
                public short Index;
            }
            
            [TagStructure(Size = 0x2)]
            public class MaterialTypeStruct : TagStructure
            {
                public short GlobalMaterialIndex;
            }
        }
    }
}
