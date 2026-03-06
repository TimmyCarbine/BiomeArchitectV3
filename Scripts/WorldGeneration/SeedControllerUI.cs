using Godot;
using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public sealed partial class SeedControllerUI : Control
    {
        [Export] private LineEdit _seedLineEdit = null!;
        [Export] private Button _randomiseButton = null!;
        [Export] private Button _regenerateButton = null!;
        [Export] private WorldManager _worldManger = null!;

        private const int DEFAULT_SEED = -2047984490; // -1633255524 All seeds clumped together

        private readonly WorldConfig _config = new WorldConfig
        {
            TerrainWidthTiles = 256,
            TerrainHeightTiles = 128,
            WrapX = true,
            Region = new RegionConfig
            {
                SkyMin01 = 0.25f,
                SkyMax01 = 0.40f,
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
            _worldManger.Regenerate(_config, s);
        }



        private void OnRegeneratePressed()
        {
            int seed = ParseSeedOrFallback(_seedLineEdit.Text, DEFAULT_SEED);
            _worldManger.Regenerate(_config, seed);
        }



        private static int ParseSeedOrFallback(string text, int fallback)
        {
            if (int.TryParse(text, out int s))
                return s;

            return fallback;
        }
    }
}