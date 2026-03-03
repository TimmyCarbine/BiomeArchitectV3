using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class BiomeMapPhase : GenerationPhase
    {
        public override string Name => "BiomeMapPhase";
        public override string StreamLabel => WorldSeedStreams.BIOME_MAP;

        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            for (int y = 0; y < context.Config.TerrainHeightTiles; y++)
            {
                for (int x = 0; x < context.Config.TerrainWidthTiles; x++)
                {
                    int biomeIndex = (x / 32 + y / 32 + (rng.Range(0, 2))) % 4;
                }
            }

            GD.Print("[BAV3] BiomeMap created");
        }
    }
}