using Godot;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.WorldGeneration;

namespace BiomeArchitectV3.Scripts.Debug.Generation
{
    public sealed partial class GenerationDebugRenderer : Node2D
    {
        [Export] public bool DebugEnabled { get; set; } = true;
        [Export] private NodePath _worldManagerPath { get; set; } = null!;

        private readonly List<GenerationDebugLayer> _layers = [];
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

            foreach (GenerationDebugLayer layer in _layers)
            {
                layer.RebuildLayer(context);
            }
        }



        public void ClearAll()
        {
            foreach (GenerationDebugLayer layer in _layers)
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
                if (child is GenerationDebugLayer layer)
                {
                    _layers.Add(layer);
                }
            }
        }



        private void ResolveWorldManagerReference()
        {
            if (_worldManagerPath == null || _worldManagerPath.IsEmpty)
            {
                GD.PrintErr("[BAV3] [GenerationDebugRenderer] - WorldManager path is not assigned");
                return;
            }

            _worldManager = GetNodeOrNull<WorldManager>(_worldManagerPath);

            if (_worldManager == null)
            {
                GD.PrintErr("[BAV3] [GenerationDebugRenderer] - Failed to resolve WorldManager");
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