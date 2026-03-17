using Godot;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Managers;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public sealed partial class UI_SeedController : Control
    {
        [Export] private LineEdit _seedLineEdit = null!;
        [Export] private Button _randomiseButton = null!;
        [Export] private Button _regenerateButton = null!;
        [Export] private M_World _worldManager = null!;

        private const int DEFAULT_SEED = 1064472210; // -1633255524 All seeds clumped together (no longer clumped) | 1064472210 Underground dominant stacked seeds | -1117072743 Sky dominant stacked seeds

        private readonly WorldConfig _config = new WorldConfig
        {
            TerrainWidthTiles = 1024,
            TerrainHeightTiles = 1024,
            WrapX = true,
            Region = new RegionConfig
            {
                SkyMin01 = 0.15f,
                SkyMax01 = 0.60f,
                SurfaceMin01 = 0.15f,
                SurfaceMax01 = 0.25f
            }
        };



        public override void _Ready()
        {
            _randomiseButton.Pressed += OnRandomisePressed;
            _regenerateButton.Pressed += OnRegeneratePressed;

            if (string.IsNullOrWhiteSpace(_seedLineEdit.Text))
                _seedLineEdit.Text = DEFAULT_SEED.ToString();

            OnRegeneratePressed();
        }



        private void OnRandomisePressed()
        {
            int s = (int)GD.Randi();
            _seedLineEdit.Text = s.ToString();
            _worldManager.Regenerate(_config, s);
        }



        private void OnRegeneratePressed()
        {
            int seed = ParseSeedOrFallback(_seedLineEdit.Text, DEFAULT_SEED);
            _worldManager.Regenerate(_config, seed);
        }



        private static int ParseSeedOrFallback(string text, int fallback)
        {
            if (int.TryParse(text, out int s))
                return s;

            return fallback;
        }
    }
}