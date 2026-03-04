using System.Collections.Generic;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class BiomeSelectionResult(IReadOnlyList<BiomeDef> sky, IReadOnlyList<BiomeDef> surface, IReadOnlyList<BiomeDef> underground)
    {
        public IReadOnlyList<BiomeDef> Sky { get; } = sky;
        public IReadOnlyList<BiomeDef> Surface { get; } = surface;
        public IReadOnlyList<BiomeDef> Underground { get; } = underground;
    }
}