using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Builders;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class BiomeMapPhase : GenerationPhase
    {
        public override string Name => "BiomeMapPhase";
        public override string StreamLabel => WorldSeedStreams.BIOME_MAP;

        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            if (context.RegionMap == null)
            {
                GD.PushError("[BAV3] BiomeMapPhase: RegionMap is null.");
                return;
            }

            if (context.BiomeSeeds.Count == 0)
            {
                GD.PushError("[BAV3] BiomeMapPhase: BiomeSeeds is empty.");
                return;
            }

            BiomeMapBuilder.Build(context.Config, context.RegionMap, context.BiomeMap, context.BiomeSeeds);
            GD.Print("[BAV3] BiomeMap created");
        }
    }
}