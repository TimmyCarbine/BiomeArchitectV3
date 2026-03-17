using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.Terrain.Data;
using BiomeArchitectV3.Scripts.Terrain.Builders;
using BiomeArchitectV3.Scripts.Terrain.Data.Structs;
using Godot;

namespace BiomeArchitectV3.Scripts.Terrain.Nodes
{
    public sealed partial class TerrainChunk : Node2D
    {
        [Export] private TileMapLayer _terrainLayer = null!;

        public S_ChunkCoord LogicalCoord { get; private set; }
        public D_Chunk ChunkData { get; private set; } = null!;



        public override void _Ready()
        {
            if (_terrainLayer == null)
                BavLogger.Error("TerrainLayer is null");
        }



        public void Initialize(D_Chunk chunkData)
        {
            ChunkData = chunkData;
            LogicalCoord = chunkData.LogicalCoord;
            Vector2I tileSize = chunkData.Context.Config.TerrainTilesSizePx;

            Position = new Vector2
            (
                chunkData.TileRect.Min.X * tileSize.X,
                chunkData.TileRect.Min.Y * tileSize.Y
            );
        }



        public void Rebuild()
        {
            if (_terrainLayer == null)
            {
                BavLogger.Error("TerrainLayer is null");
                return;
            }

            if (ChunkData == null)
            {
                BavLogger.Error("ChunkData is null");
                return;
            }
            
            Clear();
            B_ChunkTiles.Build(_terrainLayer, ChunkData);
        }



        public void Clear()
        {
            if (_terrainLayer == null)
            {
                BavLogger.Error("TerrainLayer is null");
                return;
            }

            _terrainLayer.Clear();
        }
    }
}