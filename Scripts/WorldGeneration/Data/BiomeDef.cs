using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class BiomeDef(string id, RegionId region, int selectionWeight)
    {
        public string Id { get; } = id;
        public RegionId Region { get; } = region;
        public int SelectionWeight { get; } = selectionWeight;
    }
}
