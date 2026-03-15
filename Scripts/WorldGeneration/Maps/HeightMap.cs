using BiomeArchitectV3.Scripts.Core.Math;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Maps
{
    public sealed class HeightMap(int width, bool wrapX)
    {
        private readonly int[] _surfaceY = new int[width];

        public int Width { get; } = width;
        public bool WrapX { get; } = wrapX;



        public int GetSurfaceY(int x)
        {
            x = NormalizeX(x);

            return _surfaceY[x];
        }



        public void SetSurface(int x, int y)
        {
            x = NormalizeX(x);
            _surfaceY[x] = y;
        }



        private int NormalizeX(int x)
        {
            if (!WrapX)
                return x;

            return U_Wrap.WrapX(x, Width);
        }
    }
}