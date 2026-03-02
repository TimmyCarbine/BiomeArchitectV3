using Godot;

namespace BiomeArchitectV3.Scripts.Core.World
{
    public sealed class WorldConfig
    {
        public int TerrainWidthTiles { get; init; } = 256; // Small = 1024 | Med = 2048 | Large = 4096
        public int TerrainHeightTiles { get; init; } = 128; // Small = 512 | Med = 1024 | Large = 2048

        public bool WrapX { get; init; } = true;

        public int TerrainChunkSizeTiles { get; init; } = 64;
        public int StreamRadiusChunksX { get; init; } = 3;
        public int StreamRadiusChunksY { get; init; } = 2;

        public Vector2I TerrainTilesSizePx { get; init; } = new Vector2I(32, 32);
    }
}