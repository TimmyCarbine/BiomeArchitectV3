namespace BiomeArchitectV3.Scripts.WorldGeneration.Data.Structs
{
    public readonly record struct S_BiomeInfluence(
        float Horizontal,   // Influences horizontal biome expansion (0.5f = 50% less score to grow horizontally | 1f = normal | 1.5f = 50% more score to grow horizontally)
        float Vertical      // Influences vertical biome expansion
    );
}