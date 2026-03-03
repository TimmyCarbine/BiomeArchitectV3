using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Builders;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class RegionMapPhase : GenerationPhase
    {
        public override string Name => "RegionMapPhase";
        public override string StreamLabel => WorldSeedStreams.REGIONS;

        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            RegionMapBuilder.Build(context.Config, context.RegionMap, rng);

            int height = context.Config.TerrainHeightTiles;
            int sky = 0;
            int surface = 0;
            int underground = 0;

            for (int y = 0; y < height; y++)
            {
                int r = context.RegionMap.Get(0, y);

                if (r == RegionMapBuilder.REGION_SKY)
                    sky++;
                else if (r == RegionMapBuilder.REGION_SURFACE)
                    surface++;
                else
                    underground++;
            }

            GD.Print($"[BAV3] Regions: Sky = {sky} ({(sky / (float)height):P0}) | " +
                                      $"Surface = {surface} ({surface / (float)height:P0}) | " +
                                      $"Underground = {underground} ({underground / (float)height:P0})");
        }
    }
}