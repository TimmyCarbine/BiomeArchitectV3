using Godot;
using System;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Builders
{
    public static class BiomeMapBuilder
    {
        private const float EDGE_JITTER = 0.5f;

        public static void Build(WorldConfig config, RegionMap regionMap, BiomeMap biomeMap, IReadOnlyList<BiomeSeed> biomeSeeds)
        {
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

                    List<BiomeSeed> list = region switch
                    {
                        RegionId.Sky => skySeeds,
                        RegionId.Surface => surfaceSeeds,
                        _ => undergroundSeeds
                    };

                    if (list.Count == 0)
                    {
                        biomeMap.SetBiomeId(x, y, string.Empty);
                        continue;
                    }

                    int bestIndex = 0;
                    float bestDistance = float.MaxValue;

                    for (int i = 0; i < list.Count; i++)
                    {
                        float currentDistance = InfluenceDistance(config, x, y, list[i]);

                        if (currentDistance < bestDistance)
                        {
                            bestDistance = currentDistance;
                            bestIndex = i;
                        }
                    }

                    biomeMap.SetBiomeId(x, y, list[bestIndex].Biome.Id);
                }
            }
        }



        // private static long DistanceSquared(WorldConfig config, int x, int y, int sx, int sy)
        // {
        //     int dx = Math.Abs(x - sx);

        //     if (config.WrapX)
        //     {
        //         int w = config.TerrainWidthTiles;
        //         int wrapped = w - dx;
        //         if (wrapped < dx)
        //             dx = wrapped;
        //     }

        //     int dy = Math.Abs(y - sy);
            
        //     return (long)dx * dx + (long)dy * dy;
        // }



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

            float hx = seed.Biome.HorizontalInfluence;
            float vy = seed.Biome.VerticalInfluence;

            float dx2 = dx * dx * hx;
            float dy2 = dy * dy * vy;

            return dx2 + dy2;
        }
    }
}