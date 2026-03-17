using BiomeArchitectV3.Scripts.Terrain.Data;
using BiomeArchitectV3.Scripts.Terrain.Data.Structs;
using BiomeArchitectV3.Scripts.Terrain.Logic;
using Godot;

namespace BiomeArchitectV3.Scripts.Terrain.Builders
{
    public static class B_ChunkTiles
    {
        public static void Build(TileMapLayer layer, D_Chunk chunk)
        {
            S_ChunkRect rect = chunk.TileRect;

            for (int y = rect.Min.Y; y <= rect.Max.Y; y++)
            {
                for (int x = rect.Min.X; x <= rect.Max.X; x++)
                {
                    S_TileVisual visual = L_TerrainTile.Resolve(chunk.Context, x, y);

                    if (visual.IsEmpty)
                        continue;

                    Vector2I local = new(x - rect.Min.X, y - rect.Min.Y);

                    layer.SetCell(local, visual.SourceId, visual.AtlasCoords, visual.AltTile);
                }
            }
        }
    }
}