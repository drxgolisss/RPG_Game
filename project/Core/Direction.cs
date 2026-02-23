namespace ConsoleRpgStage1.Core;

public sealed class Direction
{
    private Direction(int deltaRow, int deltaCol)
    {
        DeltaRow = deltaRow;
        DeltaCol = deltaCol;
    }

    public int DeltaRow { get; }

    public int DeltaCol { get; }

    public static readonly Direction Up = new(-1, 0);
    public static readonly Direction Down = new(1, 0);
    public static readonly Direction Left = new(0, -1);
    public static readonly Direction Right = new(0, 1);
}
