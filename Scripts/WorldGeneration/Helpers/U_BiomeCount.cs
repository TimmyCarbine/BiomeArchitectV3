using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;
using Godot;


namespace BiomeArchitectV3.Scripts.WorldGeneration.Helpers
{
    public static class U_BiomeCount
    {
        private const float SKY_DENSITY = 0.27f;
        private const float SURFACE_DENSITY = 0.26f;
        private const float UNDERGROUND_DENSITY = 0.31f;

        private const int JITTER_AMOUNT = 1;



        public static RegionBiomeCounts Calculate(RegionMap regionMap, U_DetermRng rng)
        {
            CountRegionRows(regionMap, out int skyRows, out int surfaceRows, out int undergroundRows);

            int sky = CalculateRegionCount(rng, skyRows, SKY_DENSITY);
            int surface = CalculateRegionCount(rng, surfaceRows, SURFACE_DENSITY);
            int underground = CalculateRegionCount(rng, undergroundRows, UNDERGROUND_DENSITY);

            return new RegionBiomeCounts(sky, surface, underground);
        }



        private static void CountRegionRows(RegionMap regionMap, out int sky, out int surface, out int underground)
        {
            sky = 0;
            surface = 0;
            underground = 0;

            for (int y = 0; y < regionMap.Height; y++)
            {
                RegionId region = regionMap.GetRegion(0, y);

                switch (region)
                {
                    case RegionId.Sky:
                        sky++;
                        break;
                    case RegionId.Surface:
                        surface++;
                        break;
                    case RegionId.Underground:
                        underground++;
                        break;
                }
            }
        }



        private static int CalculateRegionCount(U_DetermRng rng, int regionRows, float density)
        {
            if (regionRows <= 0)
                return 0;

            float baseCount = Mathf.Sqrt(regionRows) * density;
            int count = Mathf.RoundToInt(baseCount);
            count += rng.Range(-JITTER_AMOUNT, JITTER_AMOUNT + 1);

            return Mathf.Max(1, count);
        }
    }
}