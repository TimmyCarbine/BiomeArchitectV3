namespace BiomeArchitectV3.Scripts.Core.World
{
    public sealed class RegionConfig
    {
        public float SkyMin01 { get; init; } = 0.25f; // Sky 25% - 40% of Map
        public float SkyMax01 { get; init; } = 0.40f;
        public float SurfaceMin01 { get; init; } = 0.15f; // Surface 15% - 30% of Map
        public float SurfaceMax01 { get; init; } = 0.30f;
                                                            // Underground 30% - 60% of Map
    }
}