using System.Collections.Generic;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public sealed class PhaseContext(WorldConfig config, WorldSeed seed)
    {
        public WorldConfig Config { get; } = config;
        public WorldSeed Seed { get; } = seed;

        public PhaseTimings Timings { get; } = new PhaseTimings();

        public RegionMap RegionMap { get; } = new RegionMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
        public BiomeMap BiomeMap { get; } = new BiomeMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
        public HeightMap HeightMap { get; } = new HeightMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
        public SolidMap SolidMap { get; } = new SolidMap(config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);

        public BiomeSelectionResult SelectedBiomes { get; set; } = null!;
        public List<BiomeSeed> BiomeSeeds { get; } = [];
    }
}