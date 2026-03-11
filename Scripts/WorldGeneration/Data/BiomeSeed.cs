using Godot;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class BiomeSeed(D_Biome biome, Vector2I position)
    {
        public D_Biome Biome { get; } = biome;
        public Vector2I Position { get; } = position;
    }
}