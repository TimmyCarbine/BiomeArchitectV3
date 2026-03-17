using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Builders
{
    public static class B_SolidMap
    {
        public static void Build(PhaseContext context)
        {
            WorldConfig config = context.Config;
            HeightMap heightMap = context.HeightMap;
            SolidMap solidMap = context.SolidMap;

            int solidCount = 0;
            int airCount = 0;

            for (int x = 0; x < config.TerrainWidthTiles; x++)
            {
                int surfaceY = heightMap.GetSurfaceY(x);

                for (int y = 0; y < config.TerrainHeightTiles; y++)
                {
                    bool isSolid = y >= surfaceY;
                    solidMap.SetSolid(x, y, isSolid);

                    if (isSolid)
                        solidCount++;
                    else
                        airCount++;
                }
            }

            BavLogger.Init($"SolidMap created | Solid = {solidCount} tiles | Air = {airCount} tiles");
        }
    }
}