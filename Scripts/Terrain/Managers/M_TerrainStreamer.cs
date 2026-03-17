using System.Collections.Generic;
using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.Terrain.Data;
using BiomeArchitectV3.Scripts.Terrain.Logic;
using BiomeArchitectV3.Scripts.Terrain.Data.Structs;
using BiomeArchitectV3.Scripts.Terrain.Nodes;
using BiomeArchitectV3.Scripts.WorldGeneration;
using BiomeArchitectV3.Scripts.WorldGeneration.Managers;
using Godot;

namespace BiomeArchitectV3.Scripts.Terrain.Managers
{
    public sealed partial class M_TerrainStreamer : Node2D
    {
        [Export] private M_World _worldManager = null!;
        [Export] private Node2D _chunkRoot = null!;
        [Export] private PackedScene _terrainChunkScene = null!;
        [Export] private Camera2D _camera = null!;

        private readonly Dictionary<S_ChunkCoord, TerrainChunk> _activeChunks = [];

        private PhaseContext _context = null!;
        private bool _hasWorld;
        private S_ChunkCoord _lastCenterChunk;



        public override void _Ready()
        {
            if (_worldManager == null)
            {
                BavLogger.Error("WorldManager is null");
                return;
            }
            if (_chunkRoot == null)
            {
                BavLogger.Error("ChunkRoot is null");
                return;
            }
            if (_terrainChunkScene == null)
            {
                BavLogger.Error("TerrainChunkScene is null");
                return;
            }
            if (_camera == null)
            {
                BavLogger.Error("Camera is null");
                return;
            }

            _worldManager .WorldRegenerated += OnWorldRegenerated;

            if (_worldManager.LastContext != null)
                OnWorldRegenerated(_worldManager.LastContext);
        }



        public override void _ExitTree()
        {
            if (_worldManager != null)
                _worldManager.WorldRegenerated -= OnWorldRegenerated;
        }



        public override void _Process(double delta)
        {
            if (!_hasWorld)
                return;

            S_ChunkCoord centerChunk = GetCenterChunkCoord();

            if (centerChunk == _lastCenterChunk)
                return;

            _lastCenterChunk = centerChunk;
            RefreshVisibleChunks();
        }



        private void OnWorldRegenerated(PhaseContext context)
        {
            _context = context;
            _hasWorld = true;

            ClearAllChunks();

            _lastCenterChunk = GetCenterChunkCoord();
            RefreshVisibleChunks();

            BavLogger.Init("Rebuilt visible chunks");
        }



        private void RefreshVisibleChunks()
        {
            if (!_hasWorld)
                return;

            HashSet<S_ChunkCoord> wanted = GetWantedChunks();

            RemoveUnwantedChunks(wanted);
            CreateMissingChunks(wanted);
        }



        private HashSet<S_ChunkCoord> GetWantedChunks()
        {
            HashSet<S_ChunkCoord> wanted = [];

            S_ChunkCoord center = GetCenterChunkCoord();

            int radiusX = _context.Config.StreamRadiusChunksX;
            int radiusY = _context.Config.StreamRadiusChunksY;

            for (int y = center.Y - radiusY; y <= center.Y + radiusY; y++)
            {
                for (int x = center.X - radiusX; x <= center.X + radiusX; x++)
                {
                    S_ChunkCoord logicalCoord = new(x, y);

                    if (!IsChunkRowValid(logicalCoord.Y))
                        continue;

                    wanted.Add(logicalCoord);
                }
            }

            return wanted;
        }



        private void RemoveUnwantedChunks(HashSet<S_ChunkCoord> wanted)
        {
            List<S_ChunkCoord> toRemove = [];

            foreach (KeyValuePair<S_ChunkCoord, TerrainChunk> pair in _activeChunks)
            {
                if (!wanted.Contains(pair.Key))
                    toRemove.Add(pair.Key);
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                S_ChunkCoord coord = toRemove[i];
                TerrainChunk chunk = _activeChunks[coord];

                _activeChunks.Remove(coord);
                chunk.QueueFree();
            }
        }



        private void CreateMissingChunks(HashSet<S_ChunkCoord> wanted)
        {
            foreach (S_ChunkCoord logicalCoord in wanted)
            {
                if (_activeChunks.ContainsKey(logicalCoord))
                    continue;

                D_Chunk chunkData = CreateChunkData(logicalCoord);

                TerrainChunk chunk = _terrainChunkScene.Instantiate<TerrainChunk>();
                _chunkRoot.AddChild(chunk);

                chunk.Initialize(chunkData);
                chunk.Rebuild();

                _activeChunks.Add(logicalCoord, chunk);
            }
        }



        private D_Chunk CreateChunkData(S_ChunkCoord logicalCoord)
        {
            int chunkSizeTiles = _context.Config.TerrainChunkSizeTiles;

            S_ChunkCoord canonicalCoord = L_ChunkWrap.GetCanonicalChunkCoord
            (
                logicalCoord,
                GetWorldChunkWidth(),
                _context.Config.WrapX
            );

            int minX = logicalCoord.X * chunkSizeTiles;
            int minY = logicalCoord.Y * chunkSizeTiles;
            int maxX = minX + chunkSizeTiles - 1;
            int maxY = minY + chunkSizeTiles - 1;

            S_ChunkRect tileRect = new
            (
                new S_ChunkCoord(minX, minY),
                new S_ChunkCoord(maxX, maxY)
            );

            return new D_Chunk
            (
                _context,
                logicalCoord,
                canonicalCoord,
                tileRect,
                chunkSizeTiles
            );
        }



        private S_ChunkCoord GetCenterChunkCoord()
        {
            Vector2 worldPos = _camera.GlobalPosition;
            Vector2I tileSizePx = _context.Config.TerrainTilesSizePx;
            int chunkSizeTiles = _context.Config.TerrainChunkSizeTiles;

            int tileX = Mathf.FloorToInt(worldPos.X / tileSizePx.X);
            int tileY = Mathf.FloorToInt(worldPos.Y / tileSizePx.Y);

            int chunkX = Mathf.FloorToInt((float)tileX / chunkSizeTiles);
            int chunkY = Mathf.FloorToInt((float)tileY / chunkSizeTiles);

            return new S_ChunkCoord(chunkX, chunkY);
        }



        private int GetWorldChunkWidth()
        {
            int widthTiles = _context.Config.TerrainWidthTiles;
            int chunkSizeTiles = _context.Config.TerrainChunkSizeTiles;

            return Mathf.CeilToInt((float)widthTiles / chunkSizeTiles);
        }



        private int GetWorldChunkHeight()
        {
            int heightTiles = _context.Config.TerrainHeightTiles;
            int chunkSizeTiles = _context.Config.TerrainChunkSizeTiles;

            return Mathf.CeilToInt((float)heightTiles / chunkSizeTiles);
        }



        private bool IsChunkRowValid(int chunkY)
        {
            return chunkY >= 0 && chunkY < GetWorldChunkHeight();
        }



        private void ClearAllChunks()
        {
            foreach (KeyValuePair<S_ChunkCoord, TerrainChunk> pair in _activeChunks)
                pair.Value.QueueFree();

            _activeChunks.Clear();
        }
    }
}