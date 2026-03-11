using System.Collections.Generic;
using System.Linq;
using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class D_BiomeRegistry
    {
        private readonly List<D_Biome> _all = [];



        public D_BiomeRegistry()
        {
            // ====================================================== SKY ====================================================== preferredHeightNormalized: 0 = top of region, 1 = bottom of region
            // Common
            _all.Add(new D_Biome("Sky Meadow",              RegionId.Sky,           selectionWeight: 72, preferredHeightNormalized: 1.00f, preferredHeightStrength: 0.30f)); // new Color("#2FAE5E"))
            _all.Add(new D_Biome("Cloud Forest",            RegionId.Sky,           selectionWeight: 66, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#1E8B6A"))
            _all.Add(new D_Biome("Wind-Carved Mists",       RegionId.Sky,           selectionWeight: 60, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#3A78B8"))

            // Uncommon 
            _all.Add(new D_Biome("Thunderstorm Fields",     RegionId.Sky,           selectionWeight: 58, preferredHeightNormalized: 0.45f, preferredHeightStrength: 0.45f)); // new Color("#3B3F8F"))
            _all.Add(new D_Biome("Cumulus Highlands",       RegionId.Sky,           selectionWeight: 52, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#2F7BA6")
            _all.Add(new D_Biome("Sunshower Glades",        RegionId.Sky,           selectionWeight: 48, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#3DBE79")
            _all.Add(new D_Biome("Twilight Cloudbanks",     RegionId.Sky,           selectionWeight: 42, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#7A4FA8")

            // Rare
            _all.Add(new D_Biome("Gale Blossom Fields",     RegionId.Sky,           selectionWeight: 36, preferredHeightNormalized: 0.60f, preferredHeightStrength: 0.35f)); // new Color("#D14D8B")
            _all.Add(new D_Biome("Hailstone Front",         RegionId.Sky,           selectionWeight: 34, preferredHeightNormalized: 0.40f, preferredHeightStrength: 0.50f)); // new Color("#3E6EB6")
            _all.Add(new D_Biome("Ice Crystal Drift",       RegionId.Sky,           selectionWeight: 28, preferredHeightNormalized: 0.25f, preferredHeightStrength: 0.55f)); // new Color("#2AA7C7")
            _all.Add(new D_Biome("Sunlit Stratus",          RegionId.Sky,           selectionWeight: 26, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#E2B032")
            _all.Add(new D_Biome("Sky Mangroves",           RegionId.Sky,           selectionWeight: 22, preferredHeightNormalized: 0.75f, preferredHeightStrength: 0.45f)); // new Color("#2E8C74")

            // Very Rare
            _all.Add(new D_Biome("Floating Kelp Canopy",    RegionId.Sky,           selectionWeight: 18, preferredHeightNormalized: 0.85f, preferredHeightStrength: 0.90f)); // new Color("#1F9A7D")
            _all.Add(new D_Biome("Cirrus Spires",           RegionId.Sky,           selectionWeight: 14, preferredHeightNormalized: 0.15f, preferredHeightStrength: 0.65f)); // new Color("#6B6FE0")
            _all.Add(new D_Biome("Lightning Reef",          RegionId.Sky,           selectionWeight: 12, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.55f)); // new Color("#2E66D1")

            // Super Rare
            _all.Add(new D_Biome("Moonlit Cirque",          RegionId.Sky,           selectionWeight: 10, preferredHeightNormalized: 0.15f, preferredHeightStrength: 0.90f)); // new Color("#2B2F6C")
            _all.Add(new D_Biome("Stormglass Halo",         RegionId.Sky,           selectionWeight:  8, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#3D7F86")
            _all.Add(new D_Biome("Aurora Veil",             RegionId.Sky,           selectionWeight:  6, preferredHeightNormalized: 0.05f, preferredHeightStrength: 0.80f)); // new Color("#21C7B0")

            // Extremely Rare
            _all.Add(new D_Biome("Astral Plane",            RegionId.Sky,           selectionWeight:  4, preferredHeightNormalized: 0.10f, preferredHeightStrength: 1.00f)); // new Color("#1B1E2F")
            _all.Add(new D_Biome("Rainbow Archipelago",     RegionId.Sky,           selectionWeight:  2, preferredHeightNormalized: 0.35f, preferredHeightStrength: 0.75f)); // new Color("#FF5A5A")
            

            // ====================================================== SURFACE ====================================================== preferredHeightNormalized: 0 = top of region, 1 = bottom of region
            // Common
            _all.Add(new D_Biome("Prairie",                 RegionId.Surface,       selectionWeight: 78, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#6ECF58")
            _all.Add(new D_Biome("Temperate Forest",        RegionId.Surface,       selectionWeight: 74, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#2E8B3D")
            _all.Add(new D_Biome("Meadow",                  RegionId.Surface,       selectionWeight: 70, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#9CDC4B")
            _all.Add(new D_Biome("Coniferious Forest",      RegionId.Surface,       selectionWeight: 64, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#1F6B3A")

            // Uncommon
            _all.Add(new D_Biome("Savanna",                 RegionId.Surface,       selectionWeight: 56, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#C9B54A")
            _all.Add(new D_Biome("Sandy Desert",            RegionId.Surface,       selectionWeight: 52, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#D9B06D")
            _all.Add(new D_Biome("Rolling Tundra",          RegionId.Surface,       selectionWeight: 44, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#86C8B8")
            _all.Add(new D_Biome("Karst Badlands",          RegionId.Surface,       selectionWeight: 40, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#B7774A")

            // Rare
            _all.Add(new D_Biome("Oasis",                   RegionId.Surface,       selectionWeight: 34, preferredHeightNormalized: 0.70f, preferredHeightStrength: 0.40f)); // new Color("#2BAA8B")
            _all.Add(new D_Biome("Glacial Plains",          RegionId.Surface,       selectionWeight: 26, preferredHeightNormalized: 0.20f, preferredHeightStrength: 0.60f)); // new Color("#A8E7F2")
            _all.Add(new D_Biome("Salt Flats",              RegionId.Surface,       selectionWeight: 22, preferredHeightNormalized: 0.80f, preferredHeightStrength: 0.40f)); // new Color("#D6DADD")

            // Very Rare
            _all.Add(new D_Biome("Mangrove Delta",          RegionId.Surface,       selectionWeight: 18, preferredHeightNormalized: 0.85f, preferredHeightStrength: 0.55f)); // new Color("#2F7F62")
            _all.Add(new D_Biome("Volcanic Highlands",      RegionId.Surface,       selectionWeight: 16, preferredHeightNormalized: 0.25f, preferredHeightStrength: 0.60f)); // new Color("#8B3A2E")

            // Super Rare
            _all.Add(new D_Biome("Coral Reef",              RegionId.Surface,       selectionWeight:  8, preferredHeightNormalized: 0.95f, preferredHeightStrength: 0.75f)); // new Color("#2F9BD6")

            // Extrememly Rare
            _all.Add(new D_Biome("Plasma Fields",           RegionId.Surface,       selectionWeight:  4, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#B43CFF")

            // ====================================================== UNDERGROUND ====================================================== preferredHeightNormalized: 0 = top of region, 1 = bottom of region
            // Common
            _all.Add(new D_Biome("Geode Grotto",            RegionId.Underground,   selectionWeight: 75, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#B34DFF")
            _all.Add(new D_Biome("Ironstone Warrens",       RegionId.Underground,   selectionWeight: 70, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#C06A3B")
            _all.Add(new D_Biome("Crystal Caves",           RegionId.Underground,   selectionWeight: 68, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#36C9FF")
            _all.Add(new D_Biome("Mushroom Caves",          RegionId.Underground,   selectionWeight: 62, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#E04A9A")
            _all.Add(new D_Biome("Subterrainean River",     RegionId.Underground,   selectionWeight: 60, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#2E78D6")

            // Uncommon
            _all.Add(new D_Biome("Stone Quarry",            RegionId.Underground,   selectionWeight: 48, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#C2A56B")
            _all.Add(new D_Biome("Basalt Column Caves",     RegionId.Underground,   selectionWeight: 44, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#55606B")
            _all.Add(new D_Biome("Quartz Vein Hollows",     RegionId.Underground,   selectionWeight: 46, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#E7E7F2")
            _all.Add(new D_Biome("Glowworm Grottos",        RegionId.Underground,   selectionWeight: 42, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#B8FF3C")

            // Rare
            _all.Add(new D_Biome("Frozen Tundra",           RegionId.Underground,   selectionWeight: 34, preferredHeightNormalized: 0.20f, preferredHeightStrength: 0.55f)); // new Color("#76D7FF")
            _all.Add(new D_Biome("Lava Tubes",              RegionId.Underground,   selectionWeight: 30, preferredHeightNormalized: 0.30f, preferredHeightStrength: 0.60f)); // new Color("#FF4A2E")
            _all.Add(new D_Biome("Sulphur Pits",            RegionId.Underground,   selectionWeight: 28, preferredHeightNormalized: 0.65f, preferredHeightStrength: 0.55f)); // new Color("#E6D428")
            _all.Add(new D_Biome("Toxic Caverns",           RegionId.Underground,   selectionWeight: 26, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#5BFF3A")
            _all.Add(new D_Biome("Dark Desert",             RegionId.Underground,   selectionWeight: 24, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#9B7B46")
            _all.Add(new D_Biome("Ocean Remnants",          RegionId.Underground,   selectionWeight: 22, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#B9A26A")
            _all.Add(new D_Biome("Carbon Caves",            RegionId.Underground,   selectionWeight: 20, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.00f)); // new Color("#6DA83A")

            // Very rare
            _all.Add(new D_Biome("Underground Ocean",       RegionId.Underground,   selectionWeight: 18, preferredHeightNormalized: 0.65f, preferredHeightStrength: 0.90f)); // new Color("#1EC6D6")
            _all.Add(new D_Biome("Thermal Spring Caverns",  RegionId.Underground,   selectionWeight: 18, preferredHeightNormalized: 0.55f, preferredHeightStrength: 0.55f)); // new Color("#FF7A3A")
            _all.Add(new D_Biome("Obsidian Hollows",        RegionId.Underground,   selectionWeight: 16, preferredHeightNormalized: 0.75f, preferredHeightStrength: 0.55f)); // new Color("#7C4DFF")
            _all.Add(new D_Biome("Subterrainean Swamp",     RegionId.Underground,   selectionWeight: 14, preferredHeightNormalized: 0.70f, preferredHeightStrength: 0.45f)); // new Color("#3D8B6A")
            _all.Add(new D_Biome("Blackwater Sinkholes",    RegionId.Underground,   selectionWeight: 12, preferredHeightNormalized: 0.85f, preferredHeightStrength: 0.60f)); // new Color("#2E3F5C")

            // Super rare
            _all.Add(new D_Biome("Bioluminescent Lake",     RegionId.Underground,   selectionWeight: 10, preferredHeightNormalized: 0.80f, preferredHeightStrength: 0.65f)); // new Color("#2CFFB7")
            _all.Add(new D_Biome("Abyssal Forest",          RegionId.Underground,   selectionWeight:  6, preferredHeightNormalized: 1.00f, preferredHeightStrength: 1.00f)); // new Color("#2AA14C")

            // Extrememly Rare
            _all.Add(new D_Biome("Aether Tunnels",          RegionId.Underground,   selectionWeight:  5, preferredHeightNormalized: 0.00f, preferredHeightStrength: 0.60f)); // new Color("#7AE3FF")
            _all.Add(new D_Biome("Petrafied Caves",         RegionId.Underground,   selectionWeight:  4, preferredHeightNormalized: 0.75f, preferredHeightStrength: 0.70f)); // new Color("#FF6B5B")
        }



        public IReadOnlyList<D_Biome> GetByRegion(RegionId region)
        {
            return _all.Where(b => b.Region == region).ToList();
        }
    }
}