using System;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Builders
{
    public static class RegionMapBuilder
    {
        public static void Build(WorldConfig config, RegionMap outMap, DeterministicRng rng)
        {
            int width = config.TerrainWidthTiles;
            int height = config.TerrainHeightTiles;

            var regionConfig = config.Region;

            float sky01 = Lerp(regionConfig.SkyMin01, regionConfig.SkyMax01, rng.NextFloat01());
            float surface01 = Lerp(regionConfig.SurfaceMin01, regionConfig.SurfaceMax01, rng.NextFloat01());

            int skyTiles = RoundToInt(height * sky01);
            int surfaceTiles = RoundToInt(height * surface01);

            if (skyTiles <= 0) skyTiles = 0;
            if (surfaceTiles <= 0) surfaceTiles = 0;

            skyTiles = Clamp(skyTiles, 0, height);
            surfaceTiles = Clamp(surfaceTiles, 0, height - skyTiles);

            int surfaceBoundary = skyTiles;
            int undergroundBoundary = skyTiles + surfaceTiles;

            for (int y = 0; y < height; y++)
            {
                RegionId region =
                    y < surfaceBoundary ? RegionId.Sky :
                    y < undergroundBoundary ? RegionId.Surface :
                    RegionId.Underground;

                for (int x = 0; x < width; x++)
                    outMap.Set(x, y, region);
            }
        }



        private static float Lerp(float a, float b, float t) => a + (b - a) * t;
        private static int RoundToInt(float v) => (int)MathF.Floor(v + 0.5f);
        private static int Clamp(int v, int min, int max) => Math.Min(Math.Max(v, min), max);
    }
}