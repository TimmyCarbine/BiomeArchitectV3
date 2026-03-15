using System.Collections.Generic;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class D_BiomeSelectionResult(IReadOnlyList<D_Biome> sky, IReadOnlyList<D_Biome> surface, IReadOnlyList<D_Biome> underground)
    {
        public IReadOnlyList<D_Biome> Sky { get; } = sky;
        public IReadOnlyList<D_Biome> Surface { get; } = surface;
        public IReadOnlyList<D_Biome> Underground { get; } = underground;
    }
}