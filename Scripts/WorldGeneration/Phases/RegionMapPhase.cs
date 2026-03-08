using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Builders;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class RegionMapPhase : GenerationPhase
    {
        public override string Name => "RegionMapPhase";
        public override string StreamLabel => WorldSeedStreams.REGIONS;

        public override void Execute(PhaseContext context, U_DetermRng rng)
        {
            RegionMapBuilder.Build(context.Config, context.RegionMap, rng);

            int height = context.Config.TerrainHeightTiles;
            int sky = 0;
            int surface = 0;
            int underground = 0;

            for (int y = 0; y < height; y++)
            {
                RegionId r = context.RegionMap.Get(0, y);

                if (r == RegionId.Sky)
                    sky++;
                else if (r == RegionId.Surface)
                    surface++;
                else
                    underground++;
            }

            BavLogger.Init($"[BAV3] Regions: Sky = {sky} ({sky / (float)height:P0}) | " +
                                      $"Surface = {surface} ({surface / (float)height:P0}) | " +
                                      $"Underground = {underground} ({underground / (float)height:P0})");
        }
    }
}