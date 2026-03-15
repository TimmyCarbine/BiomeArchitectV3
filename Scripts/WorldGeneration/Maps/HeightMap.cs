namespace BiomeArchitectV3.Scripts.WorldGeneration.Maps
{
    public sealed class HeightMap(int width, bool wrapX)
    {
        private readonly int[] _surfaceY = new int[width];

        public int Width { get; } = width;
        public bool WrapX { get; } = wrapX;



        public int GetSurfaceY(int x)
        {
            x = WrapXCoord(x);

            return _surfaceY[x];
        }



        public void SetSurface(int x, int y)
        {
            x = WrapXCoord(x);
            _surfaceY[x] = y;
        }



        private int WrapXCoord(int x)
        {
            if (!WrapX)
                return x;

            int wrapped = x % Width;
            if (wrapped < 0)
                wrapped += Width;

            return wrapped;
        }
    }
}