namespace BiomeArchitectV3.Scripts.Core.World
{
    public sealed class RegionConfig
    {
        public float SkyMin01 { get; init; } = 0.15f; // Sky 15% - 60% of Map
        public float SkyMax01 { get; init; } = 0.60f;
        public float SurfaceMin01 { get; init; } = 0.15f; // Surface 15% - 25% of Map
        public float SurfaceMax01 { get; init; } = 0.25f;
                                                            // Underground 15% - 70% of Map
    }
}