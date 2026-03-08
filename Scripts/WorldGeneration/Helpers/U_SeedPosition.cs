using System;
using System.Collections.Generic;
using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Helpers
{
    public static class U_SeedPosition
    {
        public static Vector2I FindSeedPosition(PhaseContext context, U_DetermRng rng, BiomeDef biome, IReadOnlyList<Vector2I> candidates)
        {
            bool found = false;
            int bestScore = int.MinValue;
            Vector2I selectedPos = default;
            RegionId region = biome.Region;

            if (candidates.Count == 0)
                throw new InvalidOperationException($"[BAV3] ERROR @ [BiomeSeedPlacementHelper] [FindSeedPosition] - Region: {region} has 0 candidates.");

            List<Vector2I> seedPositions = GetSeedPositions(context);
            HashSet<Vector2I> occupiedPositions = new(seedPositions);

            int start = rng.Range(0, candidates.Count);
            for (int i = 0; i < candidates.Count; i++)
            {
                Vector2I candidate = candidates[(start + i) % candidates.Count];

                if (occupiedPositions.Contains(candidate))
                    continue;

                int score = ScoreSeedPosition(context, biome, candidate, seedPositions);
                
                if (!found || score > bestScore)
                {
                    found = true;
                    bestScore = score;
                    selectedPos = candidate;
                }
                else if (score == bestScore && rng.NextFloat01() < 0.5f)
                {
                    selectedPos = candidate;
                }
            }
            
            if (!found)
                throw new InvalidOperationException($"[BAV3] ERROR @ [BiomeSeedPlacementHelper] [FindSeedPosition] - Failed to find position for biome: {biome.Id} in region: {biome.Region}");

            return selectedPos;
        }



        private static List<Vector2I> GetSeedPositions(PhaseContext context)
        {
            List<Vector2I> seedPositions = [];

            foreach (BiomeSeed seed in context.BiomeSeeds)
            {
                seedPositions.Add(seed.Position);
            }

            return seedPositions;
        }


        // Higher value = better score
        private static int ScoreSeedPosition(PhaseContext context, BiomeDef biome, Vector2I position, IReadOnlyList<Vector2I> seedPositions)
        {
            int score = 0;

            score += ScoreSpacing(context, position, seedPositions);
            score += ScoreHeight(context, biome, position);

            return score;
        }



        private static int ScoreSpacing(PhaseContext context, Vector2I position, IReadOnlyList<Vector2I> seedPositions)
        {
            if (seedPositions.Count == 0)
                return int.MaxValue;

            long distanceSq = GetNearestSeedDistanceSq(context, position, seedPositions);
            int score = (int)Math.Min(distanceSq, int.MaxValue);
            return score;
        }



        private static int ScoreHeight(PhaseContext context, BiomeDef biome, Vector2I position)
        {
            return 0; // TODO - Will score based on how close the pos.Y is to it's preferred height
        }



        private static long GetNearestSeedDistanceSq(PhaseContext context, Vector2I candidate, IReadOnlyList<Vector2I> seedPositions)
        {
            long nearest = long.MaxValue;

            for (int i = 0; i < seedPositions.Count; i++)
            {
                Vector2I seedPos = seedPositions[i];
                long distanceSq = DistanceSquared(context.Config, candidate.X, candidate.Y, seedPos.X, seedPos.Y);

                if (distanceSq < nearest)
                    nearest = distanceSq;
            }

            return nearest;
        }



        private static long DistanceSquared(WorldConfig config, int x, int y, int sx, int sy)
        {
            int dx = Math.Abs(x - sx);

            if (config.WrapX)
            {
                int width = config.TerrainWidthTiles;
                int wrapped = width - dx;

                if (wrapped < dx)
                    dx = wrapped;
            }

            int dy = Math.Abs(y - sy);

            return (long)dx * dx + (long)dy * dy;
        }
    }
}