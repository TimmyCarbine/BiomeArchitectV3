using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Builders;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class BiomeMapPhase : GenerationPhase
    {
        public override string Name => "BiomeMapPhase";
        public override string StreamLabel => WorldSeedStreams.BIOME_MAP;

        public override void Execute(PhaseContext context, U_DetermRng rng)
        {
            if (context.RegionMap == null)
            {
                BavLogger.Error("RegionMap is null.");
                return;
            }

            if (context.BiomeSeeds.Count == 0)
            {
                BavLogger.Error("BiomeSeeds is empty.");
                return;
            }

            BiomeMapBuilder.Build(context);
            BavLogger.Init("BiomeMap created");
        }
    }
}