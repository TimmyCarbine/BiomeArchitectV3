using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.WorldGeneration.Helpers;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class SeedBiomesPhase : GenerationPhase
    {
        public override string Name => "SeedBiomesPhase";
        public override string StreamLabel => WorldSeedStreams.BIOME_SEEDS;



        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            if (context.SelectedBiomes == null)
            {
                GD.PushError("[BAV3] [SeedBiomesPhase] [Execute] - SelectedBiomes is null.");
                return;
            }

            context.BiomeSeeds.Clear();

            SeedRegion(context, rng, context.SelectedBiomes.Sky, RegionId.Sky);
            SeedRegion(context, rng, context.SelectedBiomes.Surface, RegionId.Surface);
            SeedRegion(context, rng, context.SelectedBiomes.Underground, RegionId.Underground);

            GD.Print($"[BAV3] [SeedBiomesPhase] [Execute] - Placed {context.BiomeSeeds.Count} seeds");
        }



        private static void SeedRegion( PhaseContext context, DeterministicRng rng, IReadOnlyList<BiomeDef> biomes, RegionId region)
        {
            List<BiomeDef> orderedBiomes = [.. biomes];
            List<Vector2I> candidates = context.RegionMap.GetCoordsForRegion(region);

            for (int i = 0; i < orderedBiomes.Count; i++)
            {
                BiomeDef biome = orderedBiomes[i];
                Vector2I position = BiomeSeedPlacementHelper.FindSeedPosition(context, rng, biome, candidates);
                BiomeSeed seed = new BiomeSeed(biome, position);
                context.BiomeSeeds.Add(seed);

                PrintSeedWithLocation(biome, position);
            }
        }



        private static void PrintSeedWithLocation(BiomeDef biome, Vector2I position)
        {
            GD.Print($"[BAV3] [SeedBiomesPhase] [SeedRegion] - {biome.Region,-11} | {biome.Id,-22} @ {position}");
        }
    }
}