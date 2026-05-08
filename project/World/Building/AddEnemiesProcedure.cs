using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddEnemiesProcedure : IDungeonBuildProcedure
{
    private readonly int _count;
    private readonly Func<Enemy>[] _enemyFactories;
    private readonly Random _random;

    public AddEnemiesProcedure(int count, IEnumerable<Func<Enemy>>? enemyFactories = null, Random? random = null)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Enemy count cannot be negative.");
        }

        _count = count;
        _random = random ?? Random.Shared;
        _enemyFactories = (enemyFactories ?? GetDefaultEnemyFactories()).ToArray();

        if (_enemyFactories.Length == 0)
        {
            throw new ArgumentException("At least one enemy factory is required.", nameof(enemyFactories));
        }
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

    private Enemy CreateEnemy()
    {
        var factory = _enemyFactories[_random.Next(_enemyFactories.Length)];
        return factory();
    }

    private static Func<Enemy>[] GetDefaultEnemyFactories()
    {
        return
        [
            static () => new Enemy(health: 10, attack: 3, armor: 1)
        ];
    }
}
