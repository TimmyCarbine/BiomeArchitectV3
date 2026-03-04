using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public sealed class PhaseContext
    {
        public WorldConfig Config { get; }
        public WorldSeed Seed { get; }

        public PhaseTimings Timings { get; } = new PhaseTimings();

        public RegionMap RegionMap { get; }
        public BiomeMap BiomeMap { get; }
        public HeightMap HeightMap { get; }
        public SolidMap SolidMap { get; }

        public BiomeSelectionResult SelectedBiomes { get; set; } = null!;



        public PhaseContext(WorldConfig config, WorldSeed seed)
        {
            Config = config;
            Seed = seed;

            RegionMap = new RegionMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
            BiomeMap = new BiomeMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
            HeightMap = new HeightMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
            SolidMap = new SolidMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
        }
    }
}