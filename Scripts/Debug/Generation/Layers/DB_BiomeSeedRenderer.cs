using Godot;
using BiomeArchitectV3.Scripts.WorldGeneration;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using System.Collections.Generic;

namespace BiomeArchitectV3.Scripts.Debug.Generation.Layers
{
    public sealed partial class DB_BiomeSeedRenderer : DB_GenLayer
    {
        [Export] public float MarkerRadius { get; set; } = 24.0f;
        [Export] public bool DrawOutline { get; set; } = true;
        [Export] public float OutlineRadiusOffset { get; set; } = 2.0f;
        [Export] public float ColourAlpha { get; set; } = 0.9f;
        [Export] public Color OutlineColour { get; set; } = new Color(0f, 0f, 0f, 0.9f);
        [Export] public int OverlayZIndex { get; set; } = 110;

        private readonly List<SeedDrawEntry> _seedEntries = [];



        public override void _Ready()
        {
            ZIndex = OverlayZIndex;
        }



        protected override void Rebuild(PhaseContext context)
        {
            _seedEntries.Clear();
            ZIndex = OverlayZIndex;

            Vector2I tileSize = context.Config.TerrainTilesSizePx;

            foreach (BiomeSeed seed in context.BiomeSeeds)
            {
                float worldX = (seed.Position.X + 0.5f) * tileSize.X;
                float worldY = (seed.Position.Y + 0.5f) * tileSize.Y;

                Color fillColour = DB_ColourHasher.FromString(seed.Biome.Id, ColourAlpha);

                _seedEntries.Add(new SeedDrawEntry(new Vector2(worldX, worldY), fillColour));
            }

            QueueRedraw();
             
        }



        protected override void Clear()
        {
            _seedEntries.Clear();
            QueueRedraw();
        }



        public override void _Draw()
        {
            if (_seedEntries.Count == 0)
                return;

            float outlineRadius = MarkerRadius * OutlineRadiusOffset;

            foreach (SeedDrawEntry entry in _seedEntries)
            {
                if (DrawOutline)
                    DrawCircle(entry.WorldPosition, outlineRadius, OutlineColour);

                DrawCircle(entry.WorldPosition, MarkerRadius, entry.FillColour);
            }
        }



        private readonly struct SeedDrawEntry(Vector2 worldPosition, Color fillColour)
        {
            public Vector2 WorldPosition { get; } = worldPosition;
            public Color FillColour { get; } = fillColour;
        }
    }
}