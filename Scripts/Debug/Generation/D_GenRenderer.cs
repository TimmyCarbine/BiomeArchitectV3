using Godot;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.WorldGeneration;

namespace BiomeArchitectV3.Scripts.Debug.Generation
{
    public sealed partial class D_GenRenderer : Node2D
    {
        [Export] public bool DebugEnabled { get; set; } = true;
        [Export] private NodePath _worldManagerPath { get; set; } = null!;

        private readonly List<D_GenLayer> _layers = [];
        private WorldManager _worldManager = null!;



        public override void _Ready()
        {
            CacheLayers();
            ResolveWorldManagerReference();
            SubscribeToWorldManager();

            Visible = DebugEnabled;
        }



        public override void _ExitTree()
        {
            UnsubscribeFromWorldManager();
        }



        public void RebuildAll(PhaseContext context)
        {
            if (!DebugEnabled)
            {
                ClearAll();
                Visible = false;
                return;
            }

            Visible = true;

            foreach (D_GenLayer layer in _layers)
            {
                layer.RebuildLayer(context);
            }
        }



        public void ClearAll()
        {
            foreach (D_GenLayer layer in _layers)
            {
                layer.ClearLayer();
            }
        }



        public void SetDebugEnabled(bool enabled)
        {
            DebugEnabled = enabled;
            Visible = enabled;

            if (!enabled)
            {
                ClearAll();
            }
            else if (_worldManager != null && _worldManager.LastContext != null)
            {
                RebuildAll(_worldManager.LastContext);
            }
        }



        public void CacheLayers()
        {
            _layers.Clear();

            foreach (Node child in GetChildren())
            {
                if (child is D_GenLayer layer)
                {
                    _layers.Add(layer);
                }
            }
        }



        private void ResolveWorldManagerReference()
        {
            if (_worldManagerPath == null || _worldManagerPath.IsEmpty)
            {
                BavLogger.Error("WorldManager path is not assigned");
                return;
            }

            _worldManager = GetNodeOrNull<WorldManager>(_worldManagerPath);

            if (_worldManager == null)
            {
                BavLogger.Error("Failed to resolve WorldManager");
            }
        }



        private void SubscribeToWorldManager()
        {
            if (_worldManager == null)
                return;

            _worldManager.WorldRegenerated += OnWorldRegenerated;
        }



        private void UnsubscribeFromWorldManager()
        {
            if (_worldManager == null)
                return;

            _worldManager.WorldRegenerated -= OnWorldRegenerated;
        }



        private void OnWorldRegenerated(PhaseContext context)
        {
            RebuildAll(context);
        }
    }
}