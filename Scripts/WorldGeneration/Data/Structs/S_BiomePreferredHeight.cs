namespace BiomeArchitectV3.Scripts.WorldGeneration.Data.Structs
{
    public readonly record struct S_BiomePreferredHeight(
        float Normalized,   // Preferred height of the seed within the region 0 - 1 (0 = high | 1 = low)
        float Strength      // How strongly the seed wants to be at it's preferred height 0 - 1 (affects scoring during seed placement)
    );
}