using System;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Builders
{
    public static class BiomeMapBuilder
    {



        public static void Build(PhaseContext context)
        {
            WorldConfig config = context.Config;
            RegionMap regionMap = context.RegionMap;
            BiomeMap biomeMap = context.BiomeMap;
            IReadOnlyList<BiomeSeed> biomeSeeds = context.BiomeSeeds;

            var skySeeds = new List<BiomeSeed>();
            var surfaceSeeds = new List<BiomeSeed>();
            var undergroundSeeds = new List<BiomeSeed>();

            for (int i = 0; i < biomeSeeds.Count; i++)
            {
                var s = biomeSeeds[i];

                switch (s.Biome.Region)
                {
                    case RegionId.Sky:
                        skySeeds.Add(s);
                        break;
                    case RegionId.Surface:
                        surfaceSeeds.Add(s);
                        break;
                    case RegionId.Underground:
                        undergroundSeeds.Add(s);
                        break;
                }
            }

            for (int y = 0; y < config.TerrainHeightTiles; y++)
            {
                for (int x = 0; x < config.TerrainWidthTiles; x++)
                {
                    RegionId region = regionMap.GetRegion(x, y);

                    List<BiomeSeed> seeds = region switch
                    {
                        RegionId.Sky => skySeeds,
                        RegionId.Surface => surfaceSeeds,
                        _ => undergroundSeeds
                    };

                    if (seeds.Count == 0)
                    {
                        biomeMap.SetBiomeId(x, y, string.Empty);
                        continue;
                    }

                    int bestIndex = 0;
                    float bestDistance = float.MaxValue;

                    for (int i = 0; i < seeds.Count; i++)
                    {
                        float currentDistance = InfluenceDistance(config, x, y, seeds[i]);

                        if (currentDistance < bestDistance)
                        {
                            bestDistance = currentDistance;
                            bestIndex = i;
                        }
                    }

                    biomeMap.SetBiomeId(x, y, seeds[bestIndex].Biome.Id);
                }
            }
        }



        private static float InfluenceDistance(WorldConfig config, int x, int y, BiomeSeed seed)
        {
            int sx = seed.Position.X;
            int sy = seed.Position.Y;

            int dx = Math.Abs(x - sx);

            if (config.WrapX)
            {
                int w = config.TerrainWidthTiles;
                int wrapped = w - dx;
                if (wrapped < dx)
                    dx = wrapped;
            }

            int dy = Math.Abs(y - sy);

            float horiInfluence = seed.Biome.HorizontalInfluence;
            float vertInfluence = seed.Biome.VerticalInfluence;

            float dx2 = dx * dx * horiInfluence;
            float dy2 = dy * dy * vertInfluence;

            return dx2 + dy2;
        }
    }
}