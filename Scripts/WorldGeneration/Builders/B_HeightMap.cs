using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;
using Godot;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Builders
{
    public static class B_HeightMap
    {
        private const int BASE_RETARGET_MIN = 24;
        private const int BASE_RETARGET_MAX = 96;

        private const int HEIGHT_STEP_MAX = 1;

        private const float SURFACE_START_MIN01 = 0.35f;
        private const float SURFACE_START_MAX01 = 0.65f;

        private const float SURFACE_MIN_CLAMP01 = 0.10f;
        private const float SURFACE_MAX_CLAMP01 = 0.90f;



        public static void Build(PhaseContext context, U_DetermRng rng)
        {
            WorldConfig config = context.Config;
            RegionMap regionMap = context.RegionMap;
            BiomeMap biomeMap = context.BiomeMap;
            HeightMap heightMap = context.HeightMap;

            regionMap.GetRegionBounds(RegionId.Surface, out int surfaceTopY, out int surfaceBottomY);

            int regionHeight = surfaceBottomY - surfaceTopY;
            int minY = surfaceTopY + Mathf.RoundToInt(regionHeight * SURFACE_MIN_CLAMP01);
            int maxY = surfaceTopY + Mathf.RoundToInt(regionHeight * SURFACE_MAX_CLAMP01);

            int currentY = surfaceTopY + Mathf.RoundToInt(regionHeight * rng.Rangef(SURFACE_START_MIN01, SURFACE_START_MAX01));
            currentY = Mathf.Clamp(currentY, minY, maxY);

            int targetY = currentY;
            int stepsUntilRetarget = 0;

            for (int x = 0; x < config.TerrainWidthTiles; x++)
            {
                int biomeSampleY = surfaceTopY;
                string biomeId = biomeMap.GetBiomeId(x, biomeSampleY);

                if (!context.SelectedBiomeLookup.TryGetValue(biomeId, out D_Biome biome))
                {
                    heightMap.SetSurface(x, currentY);
                    continue;
                }

                if (stepsUntilRetarget <= 0)
                {
                    int variance = Mathf.Max(0, biome.Surface.HeightVariance);
                    int biomeMinRetarget = Mathf.Max(1, biome.Surface.RetargetMin);
                    int biomeMaxRetarget = Mathf.Max(biomeMinRetarget, biome.Surface.RetargetMax);

                    targetY = currentY + biome.Surface.HeightBias + rng.Range(-variance, variance + 1);
                    targetY = Mathf.Clamp(targetY, minY, maxY);

                    int retargetMin = Mathf.Max(1, BASE_RETARGET_MIN + biomeMinRetarget);
                    int retargetMax = Mathf.Max(retargetMin, BASE_RETARGET_MAX + biomeMaxRetarget);
                    stepsUntilRetarget = rng.Range(retargetMin, retargetMax + 1);
                }

                currentY = MoveTowards(currentY, targetY, HEIGHT_STEP_MAX);
                currentY = Mathf.Clamp(currentY, minY, maxY);

                heightMap.SetSurface(x, currentY);
                stepsUntilRetarget--;
            }

            LogHeightRange(config, heightMap);
        }



        private static int MoveTowards(int current, int target, int maxStep)
        {
            if (current < target)
                return current + maxStep;

            if (current > target)
                return current - maxStep;

            return current;
        }



        private static void LogHeightRange(WorldConfig config, HeightMap heightMap)
        {
            int minHeight = int.MaxValue;
            int maxHeight = int.MinValue;

            for (int x = 0; x < config.TerrainWidthTiles; x++)
            {
                int y = heightMap.GetSurfaceY(x);

                if (y < minHeight)
                    minHeight = y;
                if (y > maxHeight)
                    maxHeight = y;
            }

            BavLogger.Init($"Surface Height Range = {minHeight} - {maxHeight}");
        }
    }
}