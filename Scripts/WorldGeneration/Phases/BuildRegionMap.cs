using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class BuildRegionMap : GenerationPhase
    {
        public override string Name => "BuildRegionMap";
        public override string StreamLabel => WorldSeedStreams.REGIONS;

        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            for (int y = 0; y < context.Config.TerrainHeightTiles; y++)
            {
                for (int x = 0; x < context.Config.TerrainWidthTiles; x++)
                {
                    int v = rng.Chance(0.01f) ? 0 : 1;
                    context.RegionMap.Set(x, y, v);
                }
            }

            GD.Print("[BAV3] RegionMap created.");
        }
    }
}