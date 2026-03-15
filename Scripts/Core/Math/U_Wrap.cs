namespace BiomeArchitectV3.Scripts.Core.Math
{
    public static class U_Wrap
    {
        public static int WrapX(int x, int width)
        {
            if (width <= 0)
                return 0;

            int wrapped = x % width;

            if (wrapped < 0)
                wrapped += width;

            return wrapped;
        }
    }
}