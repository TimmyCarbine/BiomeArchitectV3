using BiomeArchitectV3.Scripts.Core.Math;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Maps
{
    public abstract class BaseMap<T>
    {
        public int Width { get; }
        public int Height { get; }
        public bool WrapX { get; }

        protected readonly T[] Data;



        protected BaseMap(int width, int height, bool wrapX)
        {
            Width = width;
            Height = height;
            WrapX = wrapX;
            Data = new T[width * height];
        }



        protected int NormalizeX(int x)
        {
            if (!WrapX)
                return x;

            return U_Wrap.WrapX(x, Width);
        }



        protected int ClampY(int y)
        {
            if (y < 0)
                return 0;
            if (y >= Height)
                return Height - 1;

            return y;
        }



        protected int Index(int x, int y)
        {
            x = NormalizeX(x);
            y = ClampY(y);

            return y * Width + x;
        }



        public T Get(int x, int y) => Data[Index(x, y)];
        public void Set(int x, int y, T value) => Data[Index(x, y)] = value;
    }
}