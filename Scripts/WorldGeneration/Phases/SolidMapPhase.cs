using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.WorldGeneration.Builders;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class SolidMapPhase : GenerationPhase
    {
        public override string Name => "SolidMapPhase";
        public override string StreamLabel => WorldSeedStreams.SOLID_MAP;



        public override void Execute(PhaseContext context, U_DetermRng rng)
        {
            B_SolidMap.Build(context);
            BavLogger.Init("SolidMap Established");
        }
    }
}