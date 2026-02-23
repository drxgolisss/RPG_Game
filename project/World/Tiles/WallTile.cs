namespace ConsoleRpgStage1.World.Tiles;

public sealed class WallTile : Tile
{
    public override char Symbol => '#';

    public override bool IsPassable => false;
}
