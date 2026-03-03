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



        protected int WrapXCoord(int x)
        {
            if (!WrapX)
                return x;

            int m = x % Width;
            if (m < 0)
                m += Width;
            
            return m;
        }



        protected int Index(int x, int y)
        {
            x = WrapXCoord(x);

            if (y < 0)
                y = 0;
            if (y >= Height)
                y = Height - 1;

            return y * Width + x;
        }



        public T Get(int x, int y) => Data[Index(x, y)];
        public void Set(int x, int y, T value) => Data[Index(x, y)] = value;
    }
}