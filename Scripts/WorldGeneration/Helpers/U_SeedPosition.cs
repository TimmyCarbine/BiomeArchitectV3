using System;
using System.Collections.Generic;
using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.Debug;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Helpers
{
    public static class U_SeedPosition
    {
        private const int SPACE_WEIGHT = 1000;
        private const int HEIGHT_WEIGHT = 1000;



        public static Vector2I FindSeedPosition(PhaseContext context, U_DetermRng rng, D_Biome biome, IReadOnlyList<Vector2I> candidates)
        {
            RegionId region = biome.Region;

            if (candidates.Count == 0)
            {
                BavLogger.Error($"Region: {region} has 0 candidates.");
                return default;
            }

            List<Vector2I> seedPositions = GetSeedPositions(context, region);
            HashSet<Vector2I> occupiedPositions = new(seedPositions);
            List<SeedScore> scoredCandidates = [];

            int start = rng.Range(0, candidates.Count);
            for (int i = 0; i < candidates.Count; i++)
            {
                Vector2I candidate = candidates[(start + i) % candidates.Count];

                if (occupiedPositions.Contains(candidate))
                    continue;

                SeedScore currentScore = ScoreSeedPosition(context, biome, candidate, seedPositions);
                scoredCandidates.Add(currentScore);
            }

            if (scoredCandidates.Count == 0)
            {
                BavLogger.Error($"Failed to find position for biome: {biome.Id} in region: {region}");
                return default;
            }

            scoredCandidates.Sort((a, b) => b.TotalScore.CompareTo(a.TotalScore));

            int bestTotalScore = scoredCandidates[0].TotalScore;
            List<SeedScore> tiedBestScores = [];

            for (int i = 0; i < scoredCandidates.Count; i++)
            {
                if (scoredCandidates[i].TotalScore != bestTotalScore)
                    break;

                tiedBestScores.Add(scoredCandidates[i]);
            }

            SeedScore bestScore = tiedBestScores[rng.Range(0, tiedBestScores.Count)];
            Vector2I selectedPos = bestScore.Position;

            LogTopSeedScores(biome, scoredCandidates, bestScore);

            return selectedPos;
        }



        private static void LogTopSeedScores(D_Biome biome, IReadOnlyList<SeedScore> scores, SeedScore bestScore)
        {
            BavLogger.Debug($"{biome.Region,-11} | {biome.Id,-22} | Rank 1 | {bestScore}");
            int rank = 2;

            for (int i = 0; i < scores.Count && rank <= 5; i++)
            {
                SeedScore score = scores[i];

                if (score.Position == bestScore.Position)
                    continue;

                
                BavLogger.Trace($"{biome.Region,-11} | {biome.Id,-22} | Rank {rank} | {score}");
                rank++;
            }
        }



        private static List<Vector2I> GetSeedPositions(PhaseContext context, RegionId region)
        {
            List<Vector2I> seedPositions = [];

            foreach (BiomeSeed seed in context.BiomeSeeds)
            {
                if (seed.Biome.Region == region)
                    seedPositions.Add(seed.Position);
            }

            return seedPositions;
        }


        private static SeedScore ScoreSeedPosition(PhaseContext context, D_Biome biome, Vector2I position, IReadOnlyList<Vector2I> seedPositions)
        {
            Vector2I closestSeed = Vector2I.Zero;
            int spacingScore;
            
            if (seedPositions.Count == 0)
            {
                spacingScore = SPACE_WEIGHT;
            }
            else
            {
                closestSeed = GetClosestSeedPosition(context, position, seedPositions);
                spacingScore = ScoreSpacing(context, position, closestSeed);
            }

            int preferredTileY = GetPreferredTileY(context, biome);
            int heightScore = ScoreHeight(context, biome, position);

            SeedScore score = new
            (
                position,
                closestSeed,
                preferredTileY,
                spacingScore,
                heightScore
            );

            return score;
        }



        private static Vector2I GetClosestSeedPosition(PhaseContext context, Vector2I candidate, IReadOnlyList<Vector2I> seedPositions)
        {   
            long nearest = long.MaxValue;
            Vector2I closestSeedPos = Vector2I.Zero;

            for (int i = 0; i < seedPositions.Count; i++)
            {
                Vector2I seedPos = seedPositions[i];
                long distanceSq = GetDistanceSquared(context.Config, candidate.X, candidate.Y, seedPos.X, seedPos.Y);

                if (distanceSq < nearest)
                {
                    nearest = distanceSq;
                    closestSeedPos = seedPos;
                }
            }

            return closestSeedPos;
        }



        private static int GetPreferredTileY(PhaseContext context, D_Biome biome)
        {
            int tileY = 0;

            context.RegionMap.GetRegionBounds(biome.Region, out int topY, out int bottomY);

            if (bottomY <= topY)
            {
                BavLogger.Warning($"Region bounds bottomY: {bottomY} <= topY: {topY}. Function returned topY as default. BottomY should be greater.");
                return topY;
            }

            if (biome.PreferredHeightNormalized > 0f)
            {
                float normalized = Mathf.Clamp(biome.PreferredHeightNormalized, 0f, 1f);
                int regionHeight = bottomY - topY;
                tileY = topY + Mathf.RoundToInt(regionHeight * normalized);
            }
            
            return tileY;
        }



        private static int ScoreSpacing(PhaseContext context, Vector2I position, Vector2I closestSeed)
        {
            long distanceSq = GetDistanceSquared(context.Config, position.X, position.Y, closestSeed.X, closestSeed.Y);

            int maxDx = context.Config.WrapX
                ? context.Config.TerrainWidthTiles / 2
                : context.Config.TerrainWidthTiles - 1;

            int maxDy = context.Config.TerrainHeightTiles - 1;

            long maxDistanceSq = (long)maxDx * maxDx + (long)maxDy * maxDy;

            if (maxDistanceSq <= 0)
                return 0;

            float normalized = (float)distanceSq / maxDistanceSq;
            normalized = Mathf.Clamp(normalized, 0f, 1f);

            return Math.Clamp(Mathf.RoundToInt(normalized * SPACE_WEIGHT), 0, 99999);
        }



        private static long GetDistanceSquared(WorldConfig config, int x, int y, int sx, int sy)
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



        private static int ScoreHeight(PhaseContext context, D_Biome biome, Vector2I position)
        {
            context.RegionMap.GetRegionBounds(biome.Region, out int topY, out int bottomY);

            float candidateHeightNormalized = CalculateRegionHeightNormalized(position.Y, topY, bottomY);
            
            if (biome.PreferredHeightStrength <= 0f)
            {
                float distanceToMiddle = Mathf.Abs(candidateHeightNormalized - 0.5f);
                float closenessToMiddle = 1f - distanceToMiddle;
                return Mathf.RoundToInt(closenessToMiddle * 0.15f * HEIGHT_WEIGHT);
            }

            float preferredHeightNormalized = Mathf.Clamp(biome.PreferredHeightNormalized, 0f, 1f);
            float strength = Mathf.Clamp(biome.PreferredHeightStrength, 0f, 1f);

            float distance = Mathf.Abs(candidateHeightNormalized - preferredHeightNormalized);
            float closeness = 1f - distance;

            return Mathf.RoundToInt(closeness * strength * HEIGHT_WEIGHT);
        }



        private static float CalculateRegionHeightNormalized(int y, int topY, int bottomY)
        {
            if (bottomY <= topY)
                return 0f;

            float normalized = (float)(y - topY) / (bottomY - topY);
            return Mathf.Clamp(normalized, 0f, 1f);
        }
    }
}