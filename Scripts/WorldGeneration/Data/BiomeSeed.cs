using Godot;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class BiomeSeed(BiomeDef biome, Vector2I position)
    {
        public BiomeDef Biome { get; } = biome;
        public Vector2I Position { get; } = position;
    }
}