using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class SeedBiomesPhase : GenerationPhase
    {
        public override string Name => "SeedBiomesPhase";
        public override string StreamLabel => WorldSeedStreams.BIOME_SEEDS;

        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            int sx = rng.Range(0, context.Config.TerrainWidthTiles);
            int sy = rng.Range(0, context.Config.TerrainHeightTiles);

            GD.Print($"[BAV3] SeedBiomes: seed @ ({sx}, {sy})");
        }
    }
}