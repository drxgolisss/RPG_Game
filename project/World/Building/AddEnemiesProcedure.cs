using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddEnemiesProcedure : IDungeonBuildProcedure
{
    private readonly int _count;
    private readonly Random _random;

    public AddEnemiesProcedure(int count, Random? random = null)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Enemy count cannot be negative.");
        }

        _count = count;
        _random = random ?? Random.Shared;
    }

    public void Apply(World world)
    {
        if (_count == 0)
        {
            return;
        }

        var walkablePositions = DungeonPlacementHelper.GetWalkablePositions(world);
        if (walkablePositions.Count == 0)
        {
            return;
        }

        DungeonPlacementHelper.Shuffle(walkablePositions, _random);

        var placements = Math.Min(_count, walkablePositions.Count);
        for (var index = 0; index < placements; index++)
        {
            var position = walkablePositions[index];
            world.AddEnemy(position, CreateEnemy());
        }
    }

    private static Enemy CreateEnemy()
    {
        return new Enemy(health: 10, attack: 3, armor: 1);
    }
}
