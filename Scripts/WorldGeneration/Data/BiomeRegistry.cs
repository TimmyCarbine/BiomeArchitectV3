using System.Collections.Generic;
using System.Linq;
using BiomeArchitectV3.Scripts.Core.World;

namespace BiomeArchitectV3.Scripts.WorldGeneration.Data
{
    public sealed class BiomeRegistry
    {
        private readonly List<BiomeDef> _all = [];



        public BiomeRegistry()
        {
            // ====================================================== SKY ====================================================== preferredVertical01: 0 = top of band, 1 = bottom of  band
            // Common
            _all.Add(new BiomeDef("Sky Meadow",              RegionId.Sky, selectionWeight: 72)); // new Color("#2FAE5E"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Cloud Forest",            RegionId.Sky, selectionWeight: 66)); // new Color("#1E8B6A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Wind-Carved Mists",       RegionId.Sky, selectionWeight: 60)); // new Color("#3A78B8"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // Uncommon 
            _all.Add(new BiomeDef("Thunderstorm Fields",     RegionId.Sky, selectionWeight: 58)); // new Color("#3B3F8F"), preferredVertical01: 0.45f, verticalBiasStrength01: 0.45f), // biased
            _all.Add(new BiomeDef("Cumulus Highlands",       RegionId.Sky, selectionWeight: 52)); // new Color("#2F7BA6"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Sunshower Glades",        RegionId.Sky, selectionWeight: 48)); // new Color("#3DBE79"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Twilight Cloudbanks",     RegionId.Sky, selectionWeight: 42)); // new Color("#7A4FA8"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // Rare
            _all.Add(new BiomeDef("Gale Blossom Fields",     RegionId.Sky, selectionWeight: 36)); // new Color("#D14D8B"), preferredVertical01: 0.60f, verticalBiasStrength01: 0.35f), // biased
            _all.Add(new BiomeDef("Hailstone Front",         RegionId.Sky, selectionWeight: 34)); // new Color("#3E6EB6"), preferredVertical01: 0.40f, verticalBiasStrength01: 0.50f), // biased
            _all.Add(new BiomeDef("Ice Crystal Drift",       RegionId.Sky, selectionWeight: 28)); // new Color("#2AA7C7"), preferredVertical01: 0.25f, verticalBiasStrength01: 0.55f), // biased
            _all.Add(new BiomeDef("Sunlit Stratus",          RegionId.Sky, selectionWeight: 26)); // new Color("#E2B032"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Sky Mangroves",           RegionId.Sky, selectionWeight: 22)); // new Color("#2E8C74"), preferredVertical01: 0.75f, verticalBiasStrength01: 0.45f), // biased

            // Very Rare
            _all.Add(new BiomeDef("Floating Kelp Canopy",    RegionId.Sky, selectionWeight: 18)); // new Color("#1F9A7D"), preferredVertical01: 0.85f, verticalBiasStrength01: 0.50f), // biased
            _all.Add(new BiomeDef("Cirrus Spires",           RegionId.Sky, selectionWeight: 14)); // new Color("#6B6FE0"), preferredVertical01: 0.15f, verticalBiasStrength01: 0.65f), // biased
            _all.Add(new BiomeDef("Lightning Reef",          RegionId.Sky, selectionWeight: 12)); // new Color("#2E66D1"), preferredVertical01: 0.50f, verticalBiasStrength01: 0.55f), // biased

            // Super Rare
            _all.Add(new BiomeDef("Moonlit Cirque",          RegionId.Sky, selectionWeight: 10)); // new Color("#2B2F6C"), preferredVertical01: 0.15f, verticalBiasStrength01: 0.60f), // biased
            _all.Add(new BiomeDef("Stormglass Halo",         RegionId.Sky, selectionWeight:  8)); // new Color("#3D7F86"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),    // biased
            _all.Add(new BiomeDef("Aurora Veil",             RegionId.Sky, selectionWeight:  6)); // new Color("#21C7B0"), preferredVertical01: 0.05f, verticalBiasStrength01: 0.80f), // biased

            // Extremely Rare
            _all.Add(new BiomeDef("Astral Plane",            RegionId.Sky, selectionWeight:  4)); // new Color("#1B1E2F"), preferredVertical01: 0.10f, verticalBiasStrength01: 0.85f), // biased
            _all.Add(new BiomeDef("Rainbow Archipelago",     RegionId.Sky, selectionWeight:  2)); // new Color("#FF5A5A"), preferredVertical01: 0.35f, verticalBiasStrength01: 0.35f), // biased
            

            // ====================================================== SURFACE ====================================================== preferredVertical01: 0 = top of band, 1 = bottom of  band
            // Common
            _all.Add(new BiomeDef("Prairie",                 RegionId.Surface, selectionWeight: 78)); // new Color("#6ECF58"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Temperate Forest",        RegionId.Surface, selectionWeight: 74)); // new Color("#2E8B3D"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Meadow",                  RegionId.Surface, selectionWeight: 70)); // new Color("#9CDC4B"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Coniferious Forest",      RegionId.Surface, selectionWeight: 64)); // new Color("#1F6B3A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // Uncommon
            _all.Add(new BiomeDef("Savanna",                 RegionId.Surface, selectionWeight: 56)); // new Color("#C9B54A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Sandy Desert",            RegionId.Surface, selectionWeight: 52)); // new Color("#D9B06D"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Rolling Tundra",          RegionId.Surface, selectionWeight: 44)); // new Color("#86C8B8"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Karst Badlands",          RegionId.Surface, selectionWeight: 40)); // new Color("#B7774A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // Rare
            _all.Add(new BiomeDef("Oasis",                   RegionId.Surface, selectionWeight: 34)); // new Color("#2BAA8B"), preferredVertical01: 0.70f, verticalBiasStrength01: 0.40f), // biased
            _all.Add(new BiomeDef("Glacial Plains",          RegionId.Surface, selectionWeight: 26)); // new Color("#A8E7F2"), preferredVertical01: 0.20f, verticalBiasStrength01: 0.60f), // biased
            _all.Add(new BiomeDef("Salt Flats",              RegionId.Surface, selectionWeight: 22)); // new Color("#D6DADD"), preferredVertical01: 0.80f, verticalBiasStrength01: 0.40f), // biased

            // Very Rare
            _all.Add(new BiomeDef("Mangrove Delta",          RegionId.Surface, selectionWeight: 18)); // new Color("#2F7F62"), preferredVertical01: 0.85f, verticalBiasStrength01: 0.55f), // biased
            _all.Add(new BiomeDef("Volcanic Highlands",      RegionId.Surface, selectionWeight: 16)); // new Color("#8B3A2E"), preferredVertical01: 0.25f, verticalBiasStrength01: 0.60f), // biased

            // Super Rare
            _all.Add(new BiomeDef("Coral Reef",              RegionId.Surface, selectionWeight:  8)); // new Color("#2F9BD6"), preferredVertical01: 0.95f, verticalBiasStrength01: 0.75f), // biased

            // Extrememly Rare
            _all.Add(new BiomeDef("Plasma Fields",           RegionId.Surface, selectionWeight:  4)); // new Color("#B43CFF"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // ====================================================== UNDERGROUND ====================================================== preferredVertical01: 0 = top of band, 1 = bottom of  band
            // Common
            _all.Add(new BiomeDef("Geode Grotto",            RegionId.Underground, selectionWeight: 75)); // new Color("#B34DFF"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Ironstone Warrens",       RegionId.Underground, selectionWeight: 70)); // new Color("#C06A3B"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Crystal Caves",           RegionId.Underground, selectionWeight: 68)); // new Color("#36C9FF"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Mushroom Caves",          RegionId.Underground, selectionWeight: 62)); // new Color("#E04A9A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Subterrainean River",     RegionId.Underground, selectionWeight: 60)); // new Color("#2E78D6"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // Uncommon
            _all.Add(new BiomeDef("Stone Quarry",            RegionId.Underground, selectionWeight: 48)); // new Color("#C2A56B"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Basalt Column Caves",     RegionId.Underground, selectionWeight: 44)); // new Color("#55606B"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Quartz Vein Hollows",     RegionId.Underground, selectionWeight: 46)); // new Color("#E7E7F2"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Glowworm Grottos",        RegionId.Underground, selectionWeight: 42)); // new Color("#B8FF3C"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // Rare
            _all.Add(new BiomeDef("Frozen Tundra",           RegionId.Underground, selectionWeight: 34)); // new Color("#76D7FF"), preferredVertical01: 0.20f, verticalBiasStrength01: 0.55f), // biased
            _all.Add(new BiomeDef("Lava Tubes",              RegionId.Underground, selectionWeight: 30)); // new Color("#FF4A2E"), preferredVertical01: 0.30f, verticalBiasStrength01: 0.60f), // biased
            _all.Add(new BiomeDef("Sulphur Pits",            RegionId.Underground, selectionWeight: 28)); // new Color("#E6D428"), preferredVertical01: 0.65f, verticalBiasStrength01: 0.55f), // biased
            _all.Add(new BiomeDef("Toxic Caverns",           RegionId.Underground, selectionWeight: 26)); // new Color("#5BFF3A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Dark Desert",             RegionId.Underground, selectionWeight: 24)); // new Color("#9B7B46"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Ocean Remnants",          RegionId.Underground, selectionWeight: 22)); // new Color("#B9A26A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),
            _all.Add(new BiomeDef("Carbon Caves",            RegionId.Underground, selectionWeight: 20)); // new Color("#6DA83A"), preferredVertical01: 0f,    verticalBiasStrength01: 0f),

            // Very rare
            _all.Add(new BiomeDef("Underground Ocean",       RegionId.Underground, selectionWeight: 18)); // new Color("#1EC6D6"), preferredVertical01: 0.95f, verticalBiasStrength01: 0.80f), // biased
            _all.Add(new BiomeDef("Thermal Spring Caverns",  RegionId.Underground, selectionWeight: 18)); // new Color("#FF7A3A"), preferredVertical01: 0.55f, verticalBiasStrength01: 0.55f), // biased
            _all.Add(new BiomeDef("Obsidian Hollows",        RegionId.Underground, selectionWeight: 16)); // new Color("#7C4DFF"), preferredVertical01: 0.75f, verticalBiasStrength01: 0.55f), // biased
            _all.Add(new BiomeDef("Subterrainean Swamp",     RegionId.Underground, selectionWeight: 14)); // new Color("#3D8B6A"), preferredVertical01: 0.70f, verticalBiasStrength01: 0.45f), // biased
            _all.Add(new BiomeDef("Blackwater Sinkholes",    RegionId.Underground, selectionWeight: 12)); // new Color("#2E3F5C"), preferredVertical01: 0.85f, verticalBiasStrength01: 0.60f), // biased

            // Super rare
            _all.Add(new BiomeDef("Bioluminescent Lake",     RegionId.Underground, selectionWeight: 10)); // new Color("#2CFFB7"), preferredVertical01: 0.80f, verticalBiasStrength01: 0.65f), // biased
            _all.Add(new BiomeDef("Abyssal Forest",          RegionId.Underground, selectionWeight:  6)); // new Color("#2AA14C"), preferredVertical01: 0.90f, verticalBiasStrength01: 0.80f), // biased

            // Extrememly Rare
            _all.Add(new BiomeDef("Aether Tunnels",          RegionId.Underground, selectionWeight:  5)); // new Color("#7AE3FF"), preferredVertical01: 0.50f, verticalBiasStrength01: 0.60f), // biased
            _all.Add(new BiomeDef("Petrafied Caves",         RegionId.Underground, selectionWeight:  4)); // new Color("#FF6B5B"), preferredVertical01: 0.75f, verticalBiasStrength01: 0.70f), // biased
        }



        public IReadOnlyList<BiomeDef> GetByRegion(RegionId region)
        {
            return _all.Where(b => b.Region == region).ToList();
        }
    }
}