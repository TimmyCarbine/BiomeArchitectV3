using Godot;
using BiomeArchitectV3.Scripts.Debug;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using System.Collections.Generic;
using BiomeArchitectV3.Scripts.WorldGeneration.Helpers;
using System;
using System.Linq;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class SeedBiomesPhase : GenerationPhase
    {
        public override string Name => "SeedBiomesPhase";
        public override string StreamLabel => WorldSeedStreams.BIOME_SEEDS;



        public override void Execute(PhaseContext context, U_DetermRng rng)
        {
            if (context.SelectedBiomes == null)
            {
                BavLogger.Error("SelectedBiomes is null.");
                return;
            }

            context.BiomeSeeds.Clear();

            SeedRegion(context, rng, context.SelectedBiomes.Sky, RegionId.Sky);
            SeedRegion(context, rng, context.SelectedBiomes.Surface, RegionId.Surface);
            SeedRegion(context, rng, context.SelectedBiomes.Underground, RegionId.Underground);

            BavLogger.Init($"Placed {context.BiomeSeeds.Count} seeds");
        }



        private static void SeedRegion( PhaseContext context, U_DetermRng rng, IReadOnlyList<D_Biome> biomes, RegionId region)
        {
            
            List<D_Biome> orderedBiomes = SortDesc_BiomesByStrength(biomes);
            List<Vector2I> candidates = SelectCandidates(context, rng, region, biomes.Count);

            for (int i = 0; i < orderedBiomes.Count; i++)
            {
                D_Biome biome = orderedBiomes[i];
                Vector2I position = U_SeedPosition.FindSeedPosition(context, rng, biome, candidates);
                BiomeSeed seed = new BiomeSeed(biome, position);
                context.BiomeSeeds.Add(seed);

                PrintSeedWithLocation(biome, position);
            }
        }



        private static void PrintSeedWithLocation(D_Biome biome, Vector2I position)
        {
            BavLogger.Init($"{biome.Region,-11} | {biome.Id,-22} @ {position}");
        }



        private static List<D_Biome> SortDesc_BiomesByStrength(IReadOnlyList<D_Biome> biomes)
        {
            List<D_Biome> orderedBiomes = [.. biomes];
            orderedBiomes.Sort((a, b) => b.PreferredHeightStrength.CompareTo(a.PreferredHeightStrength));

            return orderedBiomes;
        }



        private static List<Vector2I> SelectCandidates(PhaseContext context, U_DetermRng rng, RegionId region, int biomeCount)
        {
            List<Vector2I> availablePositions = new(context.RegionMap.GetCoordsForRegion(region));

            int maxCandidates = biomeCount * 5;
            maxCandidates = Math.Min(maxCandidates, availablePositions.Count);

            U_List.Shuffle(availablePositions, rng);

            return availablePositions.GetRange(0, maxCandidates);
        }
    }
}