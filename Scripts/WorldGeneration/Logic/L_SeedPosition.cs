using System;
using System.Collections.Generic;
using Godot;
using BiomeArchitectV3.Scripts.Core.Math;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.WorldGeneration.Data;
using BiomeArchitectV3.Scripts.WorldGeneration.Data.Structs;
using BiomeArchitectV3.Scripts.Debug;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Logic
{
    public static class L_SeedPosition
    {
        private const int SPACE_WEIGHT = 1000;
        private const int HEIGHT_WEIGHT = 1000;
        private const float SEED_SPACING_HORIZONTAL_WEIGHT = 1.00f;
        private const float SEED_SPACING_VERTICAL_WEIGHT = 0.35f;



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
            List<S_SeedScore> scoredCandidates = [];

            int start = rng.Range(0, candidates.Count);
            for (int i = 0; i < candidates.Count; i++)
            {
                Vector2I candidate = candidates[(start + i) % candidates.Count];

                if (occupiedPositions.Contains(candidate))
                    continue;

                S_SeedScore currentScore = ScoreSeedPosition(context, biome, candidate, seedPositions);
                scoredCandidates.Add(currentScore);
            }

            if (scoredCandidates.Count == 0)
            {
                BavLogger.Error($"Failed to find position for biome: {biome.Id} in region: {region}");
                return default;
            }

            scoredCandidates.Sort((a, b) => b.TotalScore.CompareTo(a.TotalScore));

            int bestTotalScore = scoredCandidates[0].TotalScore;
            List<S_SeedScore> tiedBestScores = [];

            for (int i = 0; i < scoredCandidates.Count; i++)
            {
                if (scoredCandidates[i].TotalScore != bestTotalScore)
                    break;

                tiedBestScores.Add(scoredCandidates[i]);
            }

            S_SeedScore bestScore = tiedBestScores[rng.Range(0, tiedBestScores.Count)];
            Vector2I selectedPos = bestScore.Position;

            LogTopSeedScores(biome, scoredCandidates, bestScore);

            return selectedPos;
        }



        private static void LogTopSeedScores(D_Biome biome, IReadOnlyList<S_SeedScore> scores, S_SeedScore bestScore)
        {
            BavLogger.Debug($"{biome.Region,-11} | {biome.Id,-22} | Rank 1 | {bestScore}");
            int rank = 2;

            for (int i = 0; i < scores.Count && rank <= 5; i++)
            {
                S_SeedScore score = scores[i];

                if (score.Position == bestScore.Position)
                    continue;

                
                BavLogger.Trace($"{biome.Region,-11} | {biome.Id,-22} | Rank {rank} | {score}");
                rank++;
            }
        }



        private static List<Vector2I> GetSeedPositions(PhaseContext context, RegionId region)
        {
            List<Vector2I> seedPositions = [];

            foreach (D_BiomeSeed seed in context.BiomeSeeds)
            {
                if (seed.Biome.Region == region)
                    seedPositions.Add(seed.Position);
            }

            return seedPositions;
        }


        private static S_SeedScore ScoreSeedPosition(PhaseContext context, D_Biome biome, Vector2I position, IReadOnlyList<Vector2I> seedPositions)
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
                spacingScore = ScoreSpacing(context, biome.Region, position, closestSeed);
            }

            int preferredTileY = GetPreferredTileY(context, biome);
            int heightScore = ScoreHeight(context, biome, position);

            S_SeedScore score = new
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
            float nearest = float.MaxValue;
            Vector2I closestSeedPos = Vector2I.Zero;

            for (int i = 0; i < seedPositions.Count; i++)
            {
                Vector2I seedPos = seedPositions[i];
                float distance = GetSeedSpacingDistance(context.Config, candidate.X, candidate.Y, seedPos.X, seedPos.Y);

                if (distance < nearest)
                {
                    nearest = distance;
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

            if (biome.PreferredHeight.Normalized > 0f)
            {
                float normalized = Mathf.Clamp(biome.PreferredHeight.Normalized, 0f, 1f);
                int regionHeight = bottomY - topY;
                tileY = topY + Mathf.RoundToInt(regionHeight * normalized);
            }
            
            return tileY;
        }



        private static int ScoreSpacing(PhaseContext context, RegionId region, Vector2I position, Vector2I closestSeed)
        {
            float distance = GetSeedSpacingDistance(context.Config, position.X, position.Y, closestSeed.X, closestSeed.Y);

            int maxDx = context.Config.WrapX
                ? context.Config.TerrainWidthTiles / 2
                : context.Config.TerrainWidthTiles - 1;

            context.RegionMap.GetRegionBounds(region, out int topY, out int bottomY);
            int maxDy = bottomY - topY;

            float maxDistance = (maxDx * maxDx * SEED_SPACING_HORIZONTAL_WEIGHT) + (maxDy * maxDy * SEED_SPACING_VERTICAL_WEIGHT);

            if (maxDistance <= 0f)
                return 0;

            float normalized = distance / maxDistance;
            normalized = Mathf.Clamp(normalized, 0f, 1f);

            return Math.Clamp(Mathf.RoundToInt(normalized * SPACE_WEIGHT), 0, 99999);
        }



        private static float GetSeedSpacingDistance(WorldConfig config, int x, int y, int sx, int sy)
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

            float weightedDx = dx * dx * SEED_SPACING_HORIZONTAL_WEIGHT;
            float weightedDy = dy * dy * SEED_SPACING_VERTICAL_WEIGHT;

            return weightedDx + weightedDy;
        }



        private static int ScoreHeight(PhaseContext context, D_Biome biome, Vector2I position)
        {
            context.RegionMap.GetRegionBounds(biome.Region, out int topY, out int bottomY);

            float candidateHeightNormalized = CalculateRegionHeightNormalized(position.Y, topY, bottomY);
            
            if (biome.PreferredHeight.Strength <= 0f)
            {
                return 0;
            }

            float preferredHeightNormalized = Mathf.Clamp(biome.PreferredHeight.Normalized, 0f, 1f);
            float strength = Mathf.Clamp(biome.PreferredHeight.Strength, 0f, 1f);

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