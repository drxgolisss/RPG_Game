namespace ConsoleRpgStage1.World.Tiles;

public sealed class FloorTile : Tile
{
    public override char Symbol => '.';

    public override bool IsPassable => true;
}
