using System.Collections.Generic;

namespace BiomeArchitectV3.Scripts.Core.Math
{
    public static class U_List
    {
        public static void Shuffle<T>(List<T> list, U_DetermRng rng)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rng.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}