using Godot;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;

namespace BiomeArchitectV3.Scripts.Debug.Generation.Layers
{
    public sealed partial class DebugRegionMapRenderer : GenerationDebugLayer
    {
        [Export] public int OverlayZIndex { get; set; } = 90;
        [Export] public float OverlayAlpha { get; set; } = 0.35f;

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

            RegionMap regionMap = context.RegionMap;
            int width = regionMap.Width;
            int height = regionMap.Height;

            if ( width <= 0 || height <= 0)
            {
                GD.PrintErr("[BAV3] [DebugRegionMapRenderer] [Rebuild] - Invalid region map size");
                Clear();
                return;
            }

            Image image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    RegionId region = regionMap.GetRegion(x, y);
                    image.SetPixel(x, y, ResolveRegionColor(region));
                }
            }

            UploadImage(image);

            _sprite.Centered = false;
            _sprite.Position = Vector2.Zero;
            _sprite.Scale = new Vector2(context.Config.TerrainTilesSizePx.X, context.Config.TerrainTilesSizePx.Y);
            _sprite.ZIndex = OverlayZIndex;
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

            _sprite = GetNodeOrNull<Sprite2D>("RegionMapSprite");

            if (_sprite == null)
            {
                _sprite = new Sprite2D
                {
                    Name = "RegionMapSprite",
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



        private Color ResolveRegionColor(RegionId region)
        {
            return region switch
            {
                RegionId.Sky => new Color(0.75f, 0.75f, 0.75f, OverlayAlpha),
                RegionId.Surface => new Color(0.50f, 0.50f, 0.50f, OverlayAlpha),
                RegionId.Underground => new Color(0.25f, 0.25f, 0.25f, OverlayAlpha),
                _ => new Color(1f, 0f, 1f, OverlayAlpha)
            };
        }
    }
}