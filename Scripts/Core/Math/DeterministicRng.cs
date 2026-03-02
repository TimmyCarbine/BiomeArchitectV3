namespace BiomeArchitectV3.Scripts.Core.Math
{
    public struct DeterministicRng(int seed)
    {
        private int _state = seed;



        public int NextInt()
        {
            unchecked
            {
                int x = _state;
                x ^= x << 13;
                x ^= x >> 17;
                x ^= x << 5;
                _state = x;

                return x;
            }
        }



        public int Range(int minInclusive, int maxExclusive)
        {
            if (maxExclusive <= minInclusive)
                return minInclusive;

            int span = maxExclusive - minInclusive;

            int v = NextInt();
            if (v < 0) v = ~v;

            return minInclusive + (v % span);
        }



        public float NextFloat01()
        {
            int v = NextInt();
            if (v < 0) v = ~v;
            return (v & 0x00FFFFFF) / 166777215f;
        }



        public bool Chance(float probability01)
        {
            if (probability01 <= 0f) return false;
            if (probability01 >= 1f) return true;
            return NextFloat01() < probability01;
        }
    }
}