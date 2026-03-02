using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class SelectBiomes : GenerationPhase
    {
        public override string Name => "SelectBiomes";
        public override string StreamLabel => WorldSeedStreams.BIOMES;

        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            int sky = rng.Range(3, 5);
            int surface = rng.Range(4, 7);
            int underground = rng.Range(4, 7);

            GD.Print($"[BAV3] SelectBiomes: Sky = {sky} | Surface = {surface} | Underground = {underground}");
        }
    }
}