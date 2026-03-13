using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World.Building;

public sealed class DungeonBuilder
{
    private readonly List<IDungeonBuildProcedure> _procedures = new();

    public DungeonBuilder(int rows, int cols)
    {
        if (rows <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows), "Rows must be greater than zero.");
        }

        if (cols <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cols), "Cols must be greater than zero.");
        }

        Rows = rows;
        Cols = cols;
    }

    public int Rows { get; }

    public int Cols { get; }

    public DungeonBuilder StartWith(IDungeonStarterProcedure starterProcedure)
    {
        ArgumentNullException.ThrowIfNull(starterProcedure);

        if (_procedures.Count > 0)
        {
            throw new InvalidOperationException("Starter procedure must be the first procedure in the builder.");
        }

        _procedures.Add(starterProcedure);
        return this;
    }

    public DungeonBuilder Apply(IDungeonBuildProcedure procedure)
    {
        ArgumentNullException.ThrowIfNull(procedure);

        if (_procedures.Count == 0)
        {
            throw new InvalidOperationException("A starter procedure must be added before other dungeon build procedures.");
        }

        _procedures.Add(procedure);
        return this;
    }

    public World Build()
    {
        if (_procedures.Count == 0)
        {
            throw new InvalidOperationException("Dungeon builder requires a starter procedure.");
        }

        var world = new World(Rows, Cols, new FloorTile());

        foreach (var procedure in _procedures)
        {
            procedure.Apply(world);
        }

        return world;
    }
}
