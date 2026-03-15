using System;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Builders
{
    public static class BiomeSelector
    {
        public static D_BiomeSelectionResult SelectBiomes(U_DetermRng rng, D_BiomeRegistry registry, int skyCount, int surfaceCount, int undergroundCount)
        {
            var sky = SelectBiomesWeighted(rng, registry.GetByRegion(RegionId.Sky), skyCount);
            var surface = SelectBiomesWeighted(rng, registry.GetByRegion(RegionId.Surface), surfaceCount);
            var underground = SelectBiomesWeighted(rng, registry.GetByRegion(RegionId.Underground), undergroundCount);

            return new D_BiomeSelectionResult(sky, surface, underground);
        }



        private static List<D_Biome> SelectBiomesWeighted(U_DetermRng rng, IReadOnlyList<D_Biome> pool, int count)
        {
            int target = Math.Min(count, pool.Count);
            var working = new List<D_Biome>(pool);
            var biomes = new List<D_Biome>(target);

            for (int i = 0; i < target; i++)
            {
                int selectedIndex = GetWeightedIndex(rng, working);
                biomes.Add(working[selectedIndex]);
                working.RemoveAt(selectedIndex);
            }

            return biomes;
        }



        private static int GetWeightedIndex(U_DetermRng rng, IReadOnlyList<D_Biome> pool)
        {
            int total = 0;
            for (int i = 0; i < pool.Count; i++)
                total += pool[i].SelectionWeight;

            int roll = rng.Range(0, total);
            int cumulative = 0;

            for (int i = 0; i < pool.Count; i++)
            {
                cumulative += pool[i].SelectionWeight;
                if (roll < cumulative)
                {
                    return i;
                }
            }

            return pool.Count - 1;
        }
    }
}