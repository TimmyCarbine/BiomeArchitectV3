using Godot;

public readonly struct SeedScore(Vector2I position, Vector2I closestSeed, int preferredTileY, int spacingScore, int heightScore)
{
    public readonly Vector2I Position = position;
    public readonly Vector2I ClosestSeed = closestSeed;
    public readonly int PreferredTileY = preferredTileY;
    public readonly int SpacingScore = spacingScore;
    public readonly int HeightScore = heightScore;
    public readonly int TotalScore = spacingScore + heightScore;



    public override string ToString()
    {
        return $"Pos = {Position,-11} | Closest = {ClosestSeed,-11} | PrefY = {PreferredTileY,-4} | Spacing = {SpacingScore,-6} | Height = {HeightScore,-6} | Total = {TotalScore,-6}";
    }
}