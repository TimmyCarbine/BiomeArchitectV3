using Godot;
using BiomeArchitectV3.Scripts.Core.World;
using System.Collections.Generic;
using System;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Maps
{
    public sealed class RegionMap(int width, int height, bool wrapX) : BaseMap<RegionId>(width, height, wrapX)
    {
        private readonly Dictionary<RegionId, (int TopY, int BottomY)> _regionBounds = [];



        public RegionId GetRegion(int x, int y) => Get(x, y);
        public void SetRegion(int x, int y, RegionId region) => Set(x, y, region);



        public void GetRegionBounds(RegionId region, out int topY, out int bottomY)
        {
            var (TopY, BottomY) = _regionBounds[region];
            topY = TopY;
            bottomY = BottomY;
        }


        
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



        public void CalculateRegionBounds()
        {
            _regionBounds.Clear();

            foreach (RegionId region in Enum.GetValues(typeof(RegionId)))
            {
                bool found = false;
                int minY = int.MaxValue;
                int maxY = int.MinValue;

                List<Vector2I> coords = GetCoordsForRegion(region);

                for (int i = 0; i < coords.Count; i++)
                {
                    int y = coords[i].Y;

                    if (y < minY)
                        minY = y;

                    if (y > maxY)
                        maxY = y;

                    found = true;
                }

                if (!found)
                    continue;

                _regionBounds[region] = (minY, maxY);
            }
        }
    }
}