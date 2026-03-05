using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using System.Collections.Generic;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class SeedBiomesPhase : GenerationPhase
    {
        public override string Name => "SeedBiomesPhase";
        public override string StreamLabel => WorldSeedStreams.BIOME_SEEDS;

        private const int MAX_ATTEMPTS_PER_SEED = 10_000;



        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            if (context.SelectedBiomes == null)
            {
                GD.PushError("[BAV3] [SeedBiomesPhase] [Execute] - SelectedBiomes is null.");
                return;
            }

            context.BiomeSeeds.Clear();

            var assignedPositions = new HashSet<Vector2I>();

            SeedRegion(context, rng, context.SelectedBiomes.Sky, RegionId.Sky, assignedPositions);
            SeedRegion(context, rng, context.SelectedBiomes.Surface, RegionId.Surface, assignedPositions);
            SeedRegion(context, rng, context.SelectedBiomes.Underground, RegionId.Underground, assignedPositions);

            GD.Print($"[BAV3] [SeedBiomesPhase] [Execute] - Placed {context.BiomeSeeds.Count} seeds");
        }



        private static void SeedRegion(
            PhaseContext context,
            DeterministicRng rng,
            IReadOnlyList<BiomeDef> biomes,
            RegionId region,
            HashSet<Vector2I> assignedPositions
        )
        {
            List<Vector2I> candidates = context.RegionMap.GetCoordsForRegion(region);

            if (candidates.Count == 0)
            {
                GD.PushWarning($"[BAV3] [SeedBiomesPhase] [SeedRegion] - Region: {region} has 0 candidate chunks");
            }

            for (int i = 0; i < biomes.Count; i++)
            {
                var biome = biomes[i];
                Vector2I selectedPos = default;
                bool found = false;

                for (int attempt = 0; attempt < MAX_ATTEMPTS_PER_SEED; attempt++)
                {
                    int idx = rng.Range(0, candidates.Count);
                    var coord = candidates[idx];

                    if (assignedPositions.Contains(coord))
                        continue;
                    
                    selectedPos = coord;
                    found = true;
                    break;
                }

                if (!found)
                {
                    GD.PushWarning($"[BAV3] [SeedBiomesPhase] [SeedRegion] - Failed to place seed for biome: {biome.Id} in region: {region}");
                    continue;
                }

                assignedPositions.Add(selectedPos);
                context.BiomeSeeds.Add(new BiomeSeed(biome, selectedPos));

                GD.Print($"[BAV3] [SeedBiomesPhase] [SeedRegion] - {region,-11} | {biome.Id,-22} @ {selectedPos}");
            }
        }
    }
}