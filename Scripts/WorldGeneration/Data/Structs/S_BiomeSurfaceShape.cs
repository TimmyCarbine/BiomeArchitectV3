namespace BiomeArchitectV3.Scripts.WorldGeneration.Data.Structs
{ 
    public readonly record struct S_BiomeSurfaceShape(
        int HeightBias,         // Smaller value = higher surface height (0 = middle of region)
        int HeightVariance,     // How far the surface heights can vary (2 = small variation in height (Plains))
        int RetargetMin,        // Minimum frequency the surface changes height (48 = Cannot change height for 48 iterations)
        int RetargetMax         // Maximum frequency the surface changes height (96 = Must change height on 96th iteration)
    );
}

// Plains
// SurfaceHeightBias = 0
// SurfaceHeightVariance = 2
// SurfaceRetargetMin = 48
// SurfaceRetargetMax = 96

// Forest
// SurfaceHeightBias = 0
// SurfaceHeightVariance = 4
// SurfaceRetargetMin = 32
// SurfaceRetargetMax = 72

// Hills
// SurfaceHeightBias = -1
// SurfaceHeightVariance = 6
// SurfaceRetargetMin = 20
// SurfaceRetargetMax = 48

// Swamp
// SurfaceHeightBias = 2
// SurfaceHeightVariance = 2
// SurfaceRetargetMin = 56
// SurfaceRetargetMax = 104

// Mountains
// SurfaceHeightBias = -4
// SurfaceHeightVariance = 10
// SurfaceRetargetMin = 12
// SurfaceRetargetMax = 28