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
            //                                                                              PREFERRED HEIGHT        INFLUENCE           SURFACE SHAPE
            //                  NAME                        REGION              WEIGHT      NORMAL  STRENGTH        HORI    VERT        BIAS VAR MIN MAX
            // Common
            _all.Add(new D_Biome("Sky Meadow",              RegionId.Sky,           72,     new(1.00f, 0.30f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#2FAE5E"))
            _all.Add(new D_Biome("Cloud Forest",            RegionId.Sky,           66,     new(0.00f, 0.00f),     new(0.7f, 1.2f),     new( 0,  2, 48, 96))); // new Color("#1E8B6A"))
            _all.Add(new D_Biome("Wind-Carved Mists",       RegionId.Sky,           60,     new(0.00f, 0.00f),     new(1.0f, 1.5f),     new( 0,  2, 48, 96))); // new Color("#3A78B8"))

            // Uncommon 
            _all.Add(new D_Biome("Thunderstorm Fields",     RegionId.Sky,           58,     new(0.45f, 0.45f),     new(1.0f, 1.3f),     new( 0,  2, 48, 96))); // new Color("#3B3F8F"))
            _all.Add(new D_Biome("Cumulus Highlands",       RegionId.Sky,           52,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#2F7BA6")
            _all.Add(new D_Biome("Sunshower Glades",        RegionId.Sky,           48,     new(0.00f, 0.00f),     new(1.0f, 1.2f),     new( 0,  2, 48, 96))); // new Color("#3DBE79")
            _all.Add(new D_Biome("Twilight Cloudbanks",     RegionId.Sky,           42,     new(0.00f, 0.00f),     new(1.0f, 1.4f),     new( 0,  2, 48, 96))); // new Color("#7A4FA8")

            // Rare
            _all.Add(new D_Biome("Gale Blossom Fields",     RegionId.Sky,           36,     new(0.60f, 0.35f),     new(0.7f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#D14D8B")
            _all.Add(new D_Biome("Hailstone Front",         RegionId.Sky,           34,     new(0.40f, 0.50f),     new(1.0f, 1.1f),     new( 0,  2, 48, 96))); // new Color("#3E6EB6")
            _all.Add(new D_Biome("Ice Crystal Drift",       RegionId.Sky,           28,     new(0.25f, 0.55f),     new(1.0f, 1.3f),     new( 0,  2, 48, 96))); // new Color("#2AA7C7")
            _all.Add(new D_Biome("Sunlit Stratus",          RegionId.Sky,           26,     new(0.00f, 0.00f),     new(0.8f, 1.4f),     new( 0,  2, 48, 96))); // new Color("#E2B032")
            _all.Add(new D_Biome("Sky Mangroves",           RegionId.Sky,           22,     new(0.75f, 0.45f),     new(1.0f, 1.3f),     new( 0,  2, 48, 96))); // new Color("#2E8C74")

            // Very Rare
            _all.Add(new D_Biome("Floating Kelp Canopy",    RegionId.Sky,           18,     new(0.85f, 0.90f),     new(1.0f, 1.1f),     new( 0,  2, 48, 96))); // new Color("#1F9A7D")
            _all.Add(new D_Biome("Cirrus Spires",           RegionId.Sky,           14,     new(0.15f, 0.65f),     new(0.8f, 1.9f),     new( 0,  2, 48, 96))); // new Color("#6B6FE0")
            _all.Add(new D_Biome("Lightning Reef",          RegionId.Sky,           12,     new(0.00f, 0.55f),     new(1.0f, 2.2f),     new( 0,  2, 48, 96))); // new Color("#2E66D1")

            // Super Rare
            _all.Add(new D_Biome("Moonlit Cirque",          RegionId.Sky,           10,     new(0.15f, 0.90f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#2B2F6C")
            _all.Add(new D_Biome("Stormglass Halo",         RegionId.Sky,            8,     new(0.00f, 0.00f),     new(0.8f, 2.4f),     new( 0,  2, 48, 96))); // new Color("#3D7F86")
            _all.Add(new D_Biome("Aurora Veil",             RegionId.Sky,            6,     new(0.05f, 0.80f),     new(1.0f, 2.3f),     new( 0,  2, 48, 96))); // new Color("#21C7B0")

            // Extremely Rare
            _all.Add(new D_Biome("Astral Plane",            RegionId.Sky,            4,     new(0.10f, 1.00f),     new(1.0f, 2.6f),     new( 0,  2, 48, 96))); // new Color("#1B1E2F")
            _all.Add(new D_Biome("Rainbow Archipelago",     RegionId.Sky,            2,     new(0.35f, 0.75f),     new(1.0f, 2.2f),     new( 0,  2, 48, 96))); // new Color("#FF5A5A")
            

            // ====================================================== SURFACE ====================================================== preferredHeightNormalized: 0 = top of region, 1 = bottom of region
            //                                                                              PREFERRED HEIGHT        INFLUENCE           SURFACE SHAPE
            //                  NAME                        REGION              WEIGHT      NORMAL  STRENGTH        HORI    VERT        HEIGHT VARIANCE MIN MAX
            // Common
            _all.Add(new D_Biome("Prairie",                 RegionId.Surface,       78,     new(0.00f, 0.00f),     new(0.8f, 1.2f),     new( 0,  2, 48, 96))); // new Color("#6ECF58")
            _all.Add(new D_Biome("Temperate Forest",        RegionId.Surface,       74,     new(0.00f, 0.00f),     new(0.8f, 1.4f),     new( 0,  4, 32, 72))); // new Color("#2E8B3D")
            _all.Add(new D_Biome("Meadow",                  RegionId.Surface,       70,     new(0.00f, 0.00f),     new(0.8f, 1.4f),     new(-1,  6, 20, 48))); // new Color("#9CDC4B")
            _all.Add(new D_Biome("Coniferous Forest",       RegionId.Surface,       64,     new(0.00f, 0.00f),     new(0.8f, 1.1f),     new( 0,  2, 48, 96))); // new Color("#1F6B3A")

            // Uncommon
            _all.Add(new D_Biome("Savanna",                 RegionId.Surface,       56,     new(0.00f, 0.00f),     new(0.8f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#C9B54A")
            _all.Add(new D_Biome("Sandy Desert",            RegionId.Surface,       52,     new(0.00f, 0.00f),     new(0.8f, 1.4f),     new( 0,  2, 48, 96))); // new Color("#D9B06D")
            _all.Add(new D_Biome("Rolling Tundra",          RegionId.Surface,       44,     new(0.00f, 0.00f),     new(0.8f, 1.3f),     new( 0,  2, 48, 96))); // new Color("#86C8B8")
            _all.Add(new D_Biome("Karst Badlands",          RegionId.Surface,       40,     new(0.00f, 0.00f),     new(0.8f, 1.4f),     new(-2, 10, 12, 28))); // new Color("#B7774A")

            // Rare
            _all.Add(new D_Biome("Oasis",                   RegionId.Surface,       34,     new(0.70f, 0.40f),     new(0.8f, 1.4f),     new( 2,  2, 56, 104))); // new Color("#2BAA8B")
            _all.Add(new D_Biome("Glacial Plains",          RegionId.Surface,       26,     new(0.20f, 0.60f),     new(0.8f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#A8E7F2")
            _all.Add(new D_Biome("Salt Flats",              RegionId.Surface,       22,     new(0.80f, 0.40f),     new(0.8f, 1.4f),     new( 0,  2, 48, 96))); // new Color("#D6DADD")

            // Very Rare
            _all.Add(new D_Biome("Mangrove Delta",          RegionId.Surface,       18,     new(0.85f, 0.55f),     new(0.8f, 1.4f),     new( 0,  2, 48, 96))); // new Color("#2F7F62")
            _all.Add(new D_Biome("Volcanic Highlands",      RegionId.Surface,       16,     new(0.25f, 0.60f),     new(0.8f, 1.4f),     new( 0,  2, 48, 96))); // new Color("#8B3A2E")

            // Super Rare
            _all.Add(new D_Biome("Coral Reef",              RegionId.Surface,        8,     new(0.95f, 0.75f),     new(0.8f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#2F9BD6")

            // Extrememly Rare
            _all.Add(new D_Biome("Plasma Fields",           RegionId.Surface,        4,     new(0.00f, 0.00f),     new(0.8f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#B43CFF")

            // ====================================================== UNDERGROUND ====================================================== preferredHeightNormalized: 0 = top of region, 1 = bottom of region
            //                                                                              PREFERRED HEIGHT        INFLUENCE           SURFACE SHAPE
            //                  NAME                        REGION              WEIGHT      NORMAL  STRENGTH        HORI    VERT        HEIGHT VARIANCE MIN MAX
            // Common
            _all.Add(new D_Biome("Geode Grotto",            RegionId.Underground,   75,     new(0.00f, 0.00f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#B34DFF")
            _all.Add(new D_Biome("Ironstone Warrens",       RegionId.Underground,   70,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#C06A3B")
            _all.Add(new D_Biome("Crystal Caves",           RegionId.Underground,   68,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#36C9FF")
            _all.Add(new D_Biome("Mushroom Caves",          RegionId.Underground,   62,     new(0.00f, 0.00f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#E04A9A")
            _all.Add(new D_Biome("Subterranean River",      RegionId.Underground,   60,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#2E78D6")

            // Uncommon
            _all.Add(new D_Biome("Stone Quarry",            RegionId.Underground,   48,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#C2A56B")
            _all.Add(new D_Biome("Basalt Column Caves",     RegionId.Underground,   44,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#55606B")
            _all.Add(new D_Biome("Quartz Vein Hollows",     RegionId.Underground,   46,     new(0.00f, 0.00f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#E7E7F2")
            _all.Add(new D_Biome("Glowworm Grottos",        RegionId.Underground,   42,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#B8FF3C")

            // Rare
            _all.Add(new D_Biome("Frozen Tundra",           RegionId.Underground,   34,     new(0.20f, 0.55f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#76D7FF")
            _all.Add(new D_Biome("Lava Tubes",              RegionId.Underground,   30,     new(0.30f, 0.60f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#FF4A2E")
            _all.Add(new D_Biome("Sulphur Pits",            RegionId.Underground,   28,     new(0.65f, 0.55f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#E6D428")
            _all.Add(new D_Biome("Toxic Caverns",           RegionId.Underground,   26,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#5BFF3A")
            _all.Add(new D_Biome("Dark Desert",             RegionId.Underground,   24,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#9B7B46")
            _all.Add(new D_Biome("Ocean Remnants",          RegionId.Underground,   22,     new(0.00f, 0.00f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#B9A26A")
            _all.Add(new D_Biome("Carbon Caves",            RegionId.Underground,   20,     new(0.00f, 0.00f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#6DA83A")

            // Very rare
            _all.Add(new D_Biome("Underground Ocean",       RegionId.Underground,   18,     new(0.65f, 0.90f),     new(1.0f, 1.7f),     new( 0,  2, 48, 96))); // new Color("#1EC6D6")
            _all.Add(new D_Biome("Thermal Spring Caverns",  RegionId.Underground,   18,     new(0.55f, 0.55f),     new(1.0f, 1.6f),     new( 0,  2, 48, 96))); // new Color("#FF7A3A")
            _all.Add(new D_Biome("Obsidian Hollows",        RegionId.Underground,   16,     new(0.75f, 0.55f),     new(1.0f, 2.0f),     new( 0,  2, 48, 96))); // new Color("#7C4DFF")
            _all.Add(new D_Biome("Subterranean Swamp",      RegionId.Underground,   14,     new(0.70f, 0.45f),     new(1.0f, 1.9f),     new( 0,  2, 48, 96))); // new Color("#3D8B6A")
            _all.Add(new D_Biome("Blackwater Sinkholes",    RegionId.Underground,   12,     new(0.85f, 0.60f),     new(1.0f, 2.3f),     new( 0,  2, 48, 96))); // new Color("#2E3F5C")

            // Super rare
            _all.Add(new D_Biome("Bioluminescent Lake",     RegionId.Underground,   10,     new(0.80f, 0.65f),     new(1.0f, 2.2f),     new( 0,  2, 48, 96))); // new Color("#2CFFB7")
            _all.Add(new D_Biome("Abyssal Forest",          RegionId.Underground,    6,     new(1.00f, 1.00f),     new(1.0f, 2.6f),     new( 0,  2, 48, 96))); // new Color("#2AA14C")

            // Extrememly Rare
            _all.Add(new D_Biome("Aether Tunnels",          RegionId.Underground,    5,     new(0.00f, 0.60f),     new(1.0f, 1.0f),     new( 0,  2, 48, 96))); // new Color("#7AE3FF")
            _all.Add(new D_Biome("Petrified Caves",         RegionId.Underground,    4,     new(0.75f, 0.70f),     new(1.0f, 2.0f),     new( 0,  2, 48, 96))); // new Color("#FF6B5B")
        }



        public IReadOnlyList<D_Biome> GetByRegion(RegionId region)
        {
            return _all.Where(b => b.Region == region).ToList();
        }
    }
}