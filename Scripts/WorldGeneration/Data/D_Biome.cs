using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class D_Biome(string id, RegionId region, int selectionWeight, float preferredHeightNormalized = 0.5f, float preferredHeightStrength = 0f)
    {
        public string Id { get; } = id;
        public RegionId Region { get; } = region;
        public int SelectionWeight { get; } = selectionWeight;
        public float PreferredHeightNormalized = preferredHeightNormalized;
        public float PreferredHeightStrength = preferredHeightStrength;
    }
}
