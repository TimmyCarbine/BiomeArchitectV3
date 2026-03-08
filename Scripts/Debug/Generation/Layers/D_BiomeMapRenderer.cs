using Godot;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.WorldGeneration;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.Debug.Generation.Layers
{
    public sealed partial class D_BiomeMapRenderer : D_GenLayer
    {
        [Export] public bool ShowMissingBiomeIdsInMagenta { get; set; } = true;
        [Export] public int OverlayZIndex { get; set; } = 100;

        private Sprite2D _sprite = null!;
        private ImageTexture _texture = null!;
        private Vector2I _lastImageSize = Vector2I.Zero;



        public override void _Ready()
        {
            EnsureSprite();
        }



        protected override void Rebuild(PhaseContext context)
        {
            EnsureSprite();

            BiomeMap biomeMap = context.BiomeMap;
            int width = biomeMap.Width;
            int height = biomeMap.Height;

            if (width <= 0 || height <= 0)
            {
                BavLogger.Error("Invalid biome map size");
                Clear();
                return;
            }

            Image image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);
            var colourCache = new Dictionary<string, Color>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    string biomeId = biomeMap.GetBiomeId(x, y);
                    Color pixel = ResolvePixelColour(biomeId, colourCache);
                    image.SetPixel(x, y, pixel);
                }
            }

            UploadImage(image);
            _sprite.Centered = false;
            _sprite.Position = Vector2.Zero;
            _sprite.Scale = new Vector2(context.Config.TerrainTilesSizePx.X, context.Config.TerrainTilesSizePx.Y);
        }



        protected override void Clear()
        {
            EnsureSprite();
            _sprite.Texture = null;
            _lastImageSize = Vector2I.Zero;
        }



        private void EnsureSprite()
        {
            if (_sprite != null)
                return;

            _sprite = GetNodeOrNull<Sprite2D>("BiomeMapSprite");

            if (_sprite == null)
            {
                _sprite = new Sprite2D
                {
                    Name = "BiomeMapSprite",
                    Centered = false,
                    ZIndex = OverlayZIndex
                };

                AddChild(_sprite);
            }

            _sprite.ZIndex = OverlayZIndex;
            _sprite.TextureFilter = CanvasItem.TextureFilterEnum.Nearest;
        }



        private void UploadImage(Image image)
        {
            Vector2I imageSize = new(image.GetWidth(), image.GetHeight());

            if (_texture == null || imageSize != _lastImageSize)
            {
                _texture = ImageTexture.CreateFromImage(image);
                _lastImageSize = imageSize;
            }
            else
            {
                _texture.Update(image);
            }

            _sprite.Texture = _texture;
        }



        private Color ResolvePixelColour(string biomeId, Dictionary<string, Color> colourCache)
        {
            if (string.IsNullOrWhiteSpace(biomeId))
            {
                if (ShowMissingBiomeIdsInMagenta)
                {
                    return new Color(1f, 0f, 1f, 1f);
                }

                return new Color(0f, 0f, 0f, 0f);
            }

            if (colourCache.TryGetValue(biomeId, out Color cachedColour))
                return cachedColour;

            Color colour = D_ColourHasher.FromString(biomeId);
            colourCache[biomeId] = colour;

            return colour;
        }
    }
}