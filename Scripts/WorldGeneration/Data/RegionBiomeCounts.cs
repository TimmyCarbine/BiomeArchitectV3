namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class RegionBiomeCounts(int sky, int surface, int underground)
    {
        public int Sky { get; } = sky;
        public int Surface { get; } = surface;
        public int Underground { get; } = underground;
    }
}