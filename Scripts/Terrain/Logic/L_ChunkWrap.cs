using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Terrain.Data.Structs;

namespace BiomeArchitectV3.Scripts.Terrain.Logic
{
    public static class L_ChunkWrap
    {
        public static int WrapTileX(int x, int worldWidthTiles, bool wrapX)
        {
            if (!wrapX)
                return x;

            return U_Wrap.WrapX(x, worldWidthTiles);
        }



        public static int WrapChunkX(int chunkX, int worldChunkWidth, bool wrapX)
        {
            if (!wrapX)
                return chunkX;

            return U_Wrap.WrapX(chunkX, worldChunkWidth);
        }



        public static S_ChunkCoord GetCanonicalChunkCoords(S_ChunkCoord logicalCoord, int worldChunkWidth, bool wrapX)
        {
            if (!wrapX)
                return logicalCoord;

            return new S_ChunkCoord(WrapChunkX(logicalCoord.X, worldChunkWidth, true), logicalCoord.Y);
        }
    }
}