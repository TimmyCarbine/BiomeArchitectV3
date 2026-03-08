using System.Linq;
using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Builders;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Helpers;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Phases
{
    public sealed class SelectBiomesPhase : GenerationPhase
    {
        public override string Name => "SelectBiomesPhase";
        public override string StreamLabel => WorldSeedStreams.BIOMES;

        public override void Execute(PhaseContext context, DeterministicRng rng)
        {
            var registry = new BiomeRegistry();

            RegionBiomeCounts counts = RegionBiomeCountHelper.Calculate(context.RegionMap, rng);
            context.SelectedBiomeCounts = counts;

            var selection = BiomeSelector.SelectBiomes(rng, registry, counts.Sky, counts.Surface, counts.Underground);
            context.SelectedBiomes = selection;

            PrintSelectionWithRarity(selection, registry);
        }



        private static void PrintSelectionWithRarity(BiomeSelectionResult biomes, BiomeRegistry registry)
        {
            var skyPool = registry.GetByRegion(RegionId.Sky);
            var surfacePool = registry.GetByRegion(RegionId.Surface);
            var undergroundPool = registry.GetByRegion(RegionId.Underground);

            float skyTotal = skyPool.Sum(x => x.SelectionWeight);
            float surfaceTotal = surfacePool.Sum(x => x.SelectionWeight);
            float undergroundTotal = undergroundPool.Sum(x => x.SelectionWeight);

            GD.Print("[BAV3] | --------------- Sky ---------------- | -------------- Surface ------------- | ------------ Underground ----------- |");     

            int max = Mathf.Max(biomes.Sky.Count, Mathf.Max(biomes.Surface.Count, biomes.Underground.Count));

            for (int i = 0; i < max; i++)
            {
                string skyStr = "";
                string surfStr = "";
                string underStr = "";

                if (i < biomes.Sky.Count)
                {
                    var b = biomes.Sky[i];
                    float p = b.SelectionWeight / skyTotal * 100f;
                    skyStr = $"{b.Id,-24} {p,5:0.#}%";
                }

                if (i < biomes.Surface.Count)
                {
                    var b = biomes.Surface[i];
                    float p = b.SelectionWeight / surfaceTotal * 100f;
                    surfStr = $"{b.Id,-24} {p,5:0.#}%";
                }

                if (i < biomes.Underground.Count)
                {
                    var b = biomes.Underground[i];
                    float p = b.SelectionWeight / undergroundTotal * 100f;
                    underStr = $"{b.Id,-24} {p,5:0.#}%";
                }

                GD.Print($"[BAV3] | {skyStr,-36} | {surfStr,-36} | {underStr,-36} |");
            }

            string skyTotalStr = $"Total Sky: {biomes.Sky.Count,3}";
            string surfaceTotalStr = $"Total Surface: {biomes.Surface.Count,3}";
            string undergroundTotalStr = $"Total Underground: {biomes.Underground.Count,3}";

            GD.Print("[BAV3] ----------------------------------------------------------------------------------------------------------------------");
            GD.Print($"[BAV3] | {skyTotalStr,31}      | {surfaceTotalStr,31}      | {undergroundTotalStr,31}      |");
            GD.Print("[BAV3] ----------------------------------------------------------------------------------------------------------------------");
        }
    }
}