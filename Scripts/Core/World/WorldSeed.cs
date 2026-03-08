using BiomeArchitectV3.Scripts.Core.Math;

namespace BiomeArchitectV3.Scripts.Core.World
{
    public readonly struct WorldSeed(int value)
    {
        public int Value { get; } = value;



        public int Derive(string streamLabel)
        {
            return U_Hash.Combine(Value, streamLabel);
        }
    }
}