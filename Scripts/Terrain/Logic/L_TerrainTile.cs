using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.Terrain.Data.Structs;
using BiomeArchitectV3.Scripts.WorldGeneration;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using Godot;

namespace BiomeArchitectV3.Scripts.Terrain.Logic
{
    public static class L_TerrainTile
    {
        private const int DEFAULT_SOURCE_ID = 0;
        private static readonly Vector2I DEFAULT_ATLAS_COORDS = new(0, 0);
        private const int DEFAULT_ALT_TILE = 0;



        public static S_TileVisual Resolve(PhaseContext context, int x, int y)
        {
            if (!L_TileLookup.IsSolid(context, x, y))
                return S_TileVisual.Empty;

            return ResolveSolidTile(context, x, y);
        }



        private static S_TileVisual ResolveSolidTile(PhaseContext context, int x, int y)
        {
            D_Biome biome = L_TileLookup.GetBiome(context, x, y);
            RegionId region = L_TileLookup.GetRegion(context, x, y);

            _ = biome;
            _ = region;

            return S_TileVisual.Create(DEFAULT_SOURCE_ID, DEFAULT_ATLAS_COORDS, DEFAULT_ALT_TILE);
        }
    }
}