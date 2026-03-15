using System.Collections.Generic;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public sealed class PhaseContext(WorldConfig config, WorldSeed seed)
    {
        public WorldConfig Config   { get; } = config;
        public WorldSeed Seed       { get; } = seed;

        public PhaseTimings Timings { get; } = new PhaseTimings();

        public RegionMap RegionMap  { get; } = new RegionMap(   config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
        public BiomeMap BiomeMap    { get; } = new BiomeMap(    config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);
        public HeightMap HeightMap  { get; } = new HeightMap(   config.TerrainWidthTiles, config.WrapX);
        public SolidMap SolidMap    { get; } = new SolidMap(    config.TerrainWidthTiles, config.TerrainHeightTiles, config.WrapX);

        public D_RegionBiomeCounts SelectedBiomeCounts            { get; set; } = null!;
        public D_BiomeSelectionResult SelectedBiomes              { get; set; } = null!;
        public List<D_BiomeSeed> BiomeSeeds                       { get; } = [];
        public Dictionary<string, D_Biome> SelectedBiomeLookup  { get; } = [];



        public D_Biome GetBiomeAt(int x, int y)
        {
            string biomeId = BiomeMap.GetBiomeId(x, y);

            if (SelectedBiomeLookup.TryGetValue(biomeId, out D_Biome biome))
                return biome;

            return null;
        }
    }
}