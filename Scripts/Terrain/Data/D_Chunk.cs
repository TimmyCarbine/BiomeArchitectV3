using BiomeArchitectV3.Scripts.Terrain.Data.Structs;
using BiomeArchitectV3.Scripts.WorldGeneration;

namespace BiomeArchitectV3.Scripts.Terrain.Data
{
    public sealed class D_Chunk(
        PhaseContext context,
        S_ChunkCoord logicalCoord,
        S_ChunkCoord canonicalCoord,
        S_ChunkRect tileRect,
        int chunkSizeTiles
    )
    {
        public PhaseContext Context = context;
        public S_ChunkCoord LogicalCoord = logicalCoord;
        public S_ChunkCoord CanonicalCoord = canonicalCoord;
        public S_ChunkRect TileRect = tileRect;
        public int ChunkSizeTiles = chunkSizeTiles;
    }
}