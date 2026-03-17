using System;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.WorldGeneration;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;
using Godot;

namespace BiomeArchitectV3.Scripts.Terrain.Logic
{
    public static class L_TileLookup
    {
        public static bool IsSolid(PhaseContext context, int x, int y)
        {
            SolidMap solidMap = context.SolidMap;

            return solidMap.IsSolid(x, y);
        }



        public static D_Biome GetBiome(PhaseContext context, int x, int y)
        {
            D_Biome biome = context.GetBiomeAt(x, y);

            if (biome == null)
            {
                string biomeId = context.BiomeMap.GetBiomeId(x, y);
                Vector2I pos = new(x, y);

                BavLogger.Error($"Failed to find Biome of Biome ID: {biomeId} at Pos: {pos}"); // TODO BavLogger.Error should throw an exception to halt code cleanly
                throw new InvalidOperationException();
            }

            return biome;
        }



        public static RegionId GetRegion(PhaseContext context, int x, int y)
        {
            RegionMap regionMap = context.RegionMap;

            return regionMap.GetRegion(x, y);
        }
    }
}