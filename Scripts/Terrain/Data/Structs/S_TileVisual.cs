using Godot;

namespace BiomeArchitectV3.Scripts.Terrain.Data.Structs
{
    public readonly record struct S_TileVisual(
        int SourceId,
        Vector2I AtlasCoords,
        int AltTile,
        bool IsEmpty
    );
}