namespace ConsoleRpgStage1.World.Tiles;

public abstract class Tile
{
    public abstract char Symbol { get; }

    public abstract bool IsPassable { get; }
}
