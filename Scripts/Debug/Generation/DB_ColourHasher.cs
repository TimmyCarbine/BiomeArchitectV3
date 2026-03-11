using BiomeArchitectV3.Scripts.Core.Math;
using Godot;

namespace BiomeArchitectV3.Scripts.Debug.Generation
{
    public static class DB_ColourHasher
    {
        public static Color FromString(string id, float alpha = 0.75f)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new Color(0f, 0f, 0f, 0f);
            }

            int hash = U_Hash.Fnv1a32(id);
            hash = U_Hash.Mix(hash);

            unchecked
            {
                uint h = (uint)hash;

                float hue = (h & 0xFFFF) / 65535.0f;
                float saturation = 0.65f + ((h >> 16) & 0xFF) / 255.0f * 0.20f;
                float brightness = 0.75f + ((h >> 24) & 0xFF) / 255.0f * 0.20f;

                return Color.FromHsv(hue, saturation, brightness, alpha);
            }
        }
    }
}