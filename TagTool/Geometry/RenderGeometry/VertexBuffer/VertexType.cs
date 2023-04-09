namespace TagTool.Geometry
{
    /// <summary>
    /// Vertex types.
    /// </summary>
    public enum VertexType : byte
    {
        World,
        Rigid,
        Skinned,
        ParticleModel,
        FlatWorld,
        FlatRigid,
        FlatSkinned,
        Screen,
        Debug,
        Transparent,
        Particle,
        Contrail,
        LightVolume,
        SimpleChud,
        FancyChud,
        Decorator,
        TinyPosition,
        PatchyFog,
        Water,
        Ripple,
        Implicit,
        Beam,
        DualQuat
    }

    public enum VertexTypeReach : byte
    {
        World,
        Rigid,
        Skinned,
        ParticleModel,
        FlatWorld,
        FlatRigid,
        FlatSkinned,
        Screen,
        Debug,
        Transparent,
        Particle,
        Contrail,
        LightVolume,
        SimpleChud,
        FancyChud,
        Decorator,
        TinyPosition,
        PatchyFog,
        Water,
        Ripple,
        Implicit,
        Beam,
        WorldTesselated,
        RigidTesselated,
        SkinnedTesselated,
        ShaderCache,
        InstanceImposter,
        ObjectImposter,
        RigidCompressed,
        SkinnedCompressed,
        LightVolumePreCompiled,
        BlendshapeRigid,
        BlendshapeRigidBlendshaped,
        RigidBlendshaped,
        BlendshapeSkinned,
        BlendshapeSkinnedBlendshaped,
        SkinnedBlendshaped,
        VirtualGeometryHwtess,
        VirtualGeometryMemexport,
        PositionOnly1,
        VirtualGeometryDebug,
        BlendshaperigidCompressed,
        SkinneduncompressedBlendshaped,
        BlendshapeskinnedCompressed,
        Tracer,
        Polyart,
        Vectorart,
        RigidBoned,
        RigidBoned2uv,
        BlendshapeSkinned2uv,
        BlendshapeSkinned2uvBlendshaped,
        Skinned2uvBlendshaped,
        Polyartuv,
        BlendshapeSkinnedUncompressedBlendshaped
    }
}
