namespace BiomeArchitectV3.Scripts.WorldGeneration.Maps
{
    public sealed class SolidMap(int width, int height, bool wrapX) : BaseMap<bool>(width, height, wrapX)
    {
        public bool IsSolid(int x, int y) => Get(x, y);
        public void SetSolid(int x, int y, bool solid) => Set(x, y, solid);
    }
}