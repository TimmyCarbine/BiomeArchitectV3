using BiomeArchitectV3.Scripts.Core.Math;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public abstract class GenerationPhase
    {
        public abstract string Name { get; }
        public abstract string StreamLabel { get; }



        public abstract void Execute(PhaseContext context, U_DetermRng rng);
    }
}