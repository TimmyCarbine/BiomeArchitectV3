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
        public PhaseContext Context { get; } = context;
        public S_ChunkCoord LogicalCoord { get; } = logicalCoord;
        public S_ChunkCoord CanonicalCoord { get; } = canonicalCoord;
        public S_ChunkRect TileRect { get; } = tileRect;
        public int ChunkSizeTiles { get; } = chunkSizeTiles;
    }
}