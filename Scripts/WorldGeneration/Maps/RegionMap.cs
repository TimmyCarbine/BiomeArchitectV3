using Godot;
using BiomeArchitectV3.Scripts.Core.World;
using System.Collections.Generic;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Maps
{
    public sealed class RegionMap(int width, int height, bool wrapX) : BaseMap<RegionId>(width, height, wrapX)
    {
        public RegionId GetRegion(int x, int y) => Get(x, y);
        public void SetRegion(int x, int y, RegionId region) => Set(x, y, region);


        
        public List<Vector2I> GetCoordsForRegion(RegionId region)
        {
            var coords = new List<Vector2I>();

            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    if (Get(x, y) == region)
                        coords.Add(new Vector2I(x, y));
                }
            
            return coords;
        }
    }
}