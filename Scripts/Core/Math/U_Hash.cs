using System.Runtime.InteropServices;
using System.Text;

namespace BiomeArchitectV3.Scripts.Core.Math
{
    public static class U_Hash
    {
        // FNV-1a 32-bit
        public static int Fnv1a32(string s)
        {
            unchecked
            {
                const uint OFFSET_BASIS = 2166136261u;
                const uint PRIME = 16777619u;

                uint hash = OFFSET_BASIS;
                byte[] bytes = Encoding.UTF8.GetBytes(s);

                for (int i = 0; i < bytes.Length; i++)
                {
                    hash ^= bytes[i];
                    hash *= PRIME;
                }

                return (int)hash;
            }
        }



        public static int Mix(int x)
        {
            unchecked
            {
                uint h = (uint)x;
                h ^= h >> 16;
                h *= 0x7feb352d;
                h ^= h >> 15;
                h *= 0x846ca68b;
                h ^= h >> 16;
                return (int)h;
            }
        }



        public static int Combine(int a, int b)
        {
            unchecked
            {
                int h = a;
                h = h * 31 + b;
                return Mix(h);
            }
        }



        public static int Combine(int seed, string label)
        {
            return Combine(seed, Fnv1a32(label));
        }
    }
}