using BiomeArchitectV3.Scripts.WorldGeneration;
using BiomeArchitectV3.Scripts.WorldGeneration.Maps;
using Godot;

namespace BiomeArchitectV3.Scripts.Debug.Generation.Layers
{
    public sealed partial class DB_SolidMapRenderer : DB_GenLayer
    {
        [Export] public int OverlayZIndex { get; set; } = 95;
        [Export] public float SolidAlpha { get; set; } = 0.45f;
        [Export] public bool ShowAirAsTransparent { get; set; } = true;

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

            SolidMap solidMap = context.SolidMap;
            int width = solidMap.Width;
            int height = solidMap.Height;

            if (width <= 0 || height <= 0)
            {
                BavLogger.Error("Invalid solid map size");
                Clear();
                return;
            }

            Image image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool isSolid = solidMap.Get(x, y);
                    image.SetPixel(x, y, ResolvePixelColour(isSolid));
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

            _sprite = GetNodeOrNull<Sprite2D>("SolidMapSprite");

            if (_sprite == null)
            {
                _sprite = new Sprite2D
                {
                    Name = "SolidMapSprite",
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



        private Color ResolvePixelColour(bool isSolid)
        {
            if (isSolid)
                return new Color(0f, 0f, 0f, SolidAlpha);

            if (ShowAirAsTransparent)
                return new Color(0f, 0f, 0f, 0f);

            return new Color(1f, 1f, 1f, 0.08f);
        }
    }
}