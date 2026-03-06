namespace BiomeArchitectV3.Scripts.WorldGeneration.Maps
{
    public sealed class BiomeMap(int width, int height, bool wrapX) : BaseMap<string>(width, height, wrapX)
    {
        public string GetBiomeId(int x, int y) => Get(x, y);
        public void SetBiomeId(int x, int y, string biomeId) => Set(x, y, biomeId);
    }
}