using Godot;

namespace BiomeArchitectV3.Scripts.Terrain.Data.Structs
{
    public readonly record struct S_TileVisual(bool IsEmpty, int SourceId, Vector2I AtlasCoords, int AltTile)
    {
        public static S_TileVisual Empty => new(true, -1, new Vector2I(-1, -1), 0);



        public static S_TileVisual Create(int sourceId, Vector2I atlasCoords, int altTile = 0) => new(false, sourceId, atlasCoords, altTile);
    }
}