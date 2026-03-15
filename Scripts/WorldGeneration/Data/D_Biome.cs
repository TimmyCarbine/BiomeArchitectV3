using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data.Structs;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class D_Biome
    (
        string id,
        RegionId region,
        int selectionWeight,

        S_BiomePreferredHeight preferredHeight = default,
        S_BiomeInfluence influence = default,
        S_BiomeSurfaceShape surface = default
    )
    {
        // Biome Data
        public string Id { get; } = id;
        public RegionId Region { get; } = region;

        // Biome Selection
        public int SelectionWeight { get; } = selectionWeight;

        // Seed Location
        public S_BiomePreferredHeight PreferredHeight { get; } = preferredHeight;

        // Biome Size & Shape
        public S_BiomeInfluence Influence { get; } = influence;

        // Surface Shape
        public S_BiomeSurfaceShape Surface { get; } = surface;
    }
}
