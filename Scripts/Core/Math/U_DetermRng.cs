namespace BiomeArchitectV3.Scripts.Core.Math
{
    public struct U_DetermRng(int seed)
    {
        private uint _state = seed != 0 ? (uint)seed : 0x6D2B79F5u;



        public uint NextUInt()
        {
            unchecked
            {
                uint x = _state;
                x ^= x << 13;
                x ^= x >> 17;
                x ^= x << 5;
                _state = x;

                return x;
            }
        }



        public int NextInt()
        {
            unchecked
            {
                return (int)NextUInt();
            }
        }



        public int Range(int minInclusive, int maxExclusive)
        {
            if (maxExclusive <= minInclusive)
                return minInclusive;

            uint span = (uint)(maxExclusive - minInclusive);

            uint v = NextUInt();
            return minInclusive + (int)(v % span);
        }



        public float NextFloat01()
        {
            return NextUInt() * (1f / 4294967296f);
        }



        public bool Chance(float probability01)
        {
            if (probability01 <= 0f) return false;
            if (probability01 >= 1f) return true;
            return NextFloat01() < probability01;
        }
    }
}