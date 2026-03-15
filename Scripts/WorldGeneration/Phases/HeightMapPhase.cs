using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.WorldGeneration.Builders;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class HeightMapPhase : GenerationPhase
    {
        public override string Name => "HeightMapPhase";
        public override string StreamLabel => WorldSeedStreams.HEIGHT;



        public override void Execute(PhaseContext context, U_DetermRng rng)
        {
            B_HeightMap.Build(context, rng);
            BavLogger.Init("HeightMap Established");
        }
    }
}