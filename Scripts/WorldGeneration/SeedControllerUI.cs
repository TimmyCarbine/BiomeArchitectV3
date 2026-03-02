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

        private const int DEFAULT_SEED = -2047984490;

        private readonly WorldConfig _config = new WorldConfig
        {
            TerrainWidthTiles = 256,
            TerrainHeightTiles = 128,
            WrapX = true
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
            if (s < WorldConstants.MIN_WORLD_SEED)
                s = WorldConstants.MIN_WORLD_SEED;

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