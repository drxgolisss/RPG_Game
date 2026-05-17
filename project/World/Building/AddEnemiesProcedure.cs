using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Reactive.Notifications;
using ConsoleRpgStage1.Reactive.Species;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddEnemiesProcedure : IDungeonBuildProcedure
{
    private readonly int _count;
    private readonly Func<Enemy>[] _enemyFactories;
    private readonly EnemySpeciesDefinition[] _enemySpeciesDefinitions;
    private readonly INoiseSubject? _noiseSubject;
    private readonly Random _random;

    public AddEnemiesProcedure(
        int count,
        IEnumerable<Func<Enemy>>? enemyFactories = null,
        IEnumerable<EnemySpeciesDefinition>? enemySpeciesDefinitions = null,
        INoiseSubject? noiseSubject = null,
        Random? random = null)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Enemy count cannot be negative.");
        }

        _count = count;
        _random = random ?? Random.Shared;
        _enemyFactories = (enemyFactories ?? GetDefaultEnemyFactories()).ToArray();
        _enemySpeciesDefinitions = (enemySpeciesDefinitions ?? []).ToArray();
        _noiseSubject = noiseSubject;

        if (_enemyFactories.Length == 0 && _enemySpeciesDefinitions.Length == 0)
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

        if (_enemySpeciesDefinitions.Length > 0)
        {
            PlaceSpeciesEnemies(world, walkablePositions);
            return;
        }

        PlaceDefaultEnemies(world, walkablePositions);
    }

    private void PlaceSpeciesEnemies(World world, IReadOnlyList<Position> walkablePositions)
    {
        var positionIndex = 0;

        foreach (var definition in _enemySpeciesDefinitions)
        {
            var speciesGroup = new EnemySpeciesGroup(definition.SpeciesName, definition.ReactionStrategyFactory());

            for (var count = 0; count < definition.MinimumCount && positionIndex < walkablePositions.Count; count++)
            {
                var enemy = definition.EnemyFactory();
                enemy.SetMovementStrategy(definition.MovementStrategyFactory());
                speciesGroup.AddMember(enemy);
                _noiseSubject?.Subscribe(enemy);
                world.AddEnemy(walkablePositions[positionIndex], enemy);
                positionIndex++;
            }
        }
    }

    private void PlaceDefaultEnemies(World world, IReadOnlyList<Position> walkablePositions)
    {
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
