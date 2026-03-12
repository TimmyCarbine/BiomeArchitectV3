using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class D_Biome
    (
        string id,
        RegionId region,
        int selectionWeight,
        float preferredHeightNormalized = 0.5f,
        float preferredHeightStrength = 0f,
        float horizontalInfluence = 1f,
        float verticalInfluence = 1f
    )
    {
        // Biome Data
        public string Id { get; } = id;
        public RegionId Region { get; } = region;

        // Biome Selection
        public int SelectionWeight { get; } = selectionWeight;

        // Seed Location
        public float PreferredHeightNormalized = preferredHeightNormalized;
        public float PreferredHeightStrength = preferredHeightStrength;

        // Biome Size & Shape
        public float HorizontalInfluence = horizontalInfluence;
        public float VerticalInfluence = verticalInfluence;
    }
}
