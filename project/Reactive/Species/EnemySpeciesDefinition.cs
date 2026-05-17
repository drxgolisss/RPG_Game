using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Reactive.Movement;

namespace ConsoleRpgStage1.Reactive.Species;

public sealed class EnemySpeciesDefinition
{
    public EnemySpeciesDefinition(
        string speciesName,
        int minimumCount,
        Func<Enemy> enemyFactory,
        Func<ISpeciesReactionStrategy> reactionStrategyFactory,
        Func<IEnemyMovementStrategy> movementStrategyFactory)
    {
        if (minimumCount < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumCount), "A species must have at least two representatives.");
        }

        SpeciesName = string.IsNullOrWhiteSpace(speciesName) ? "Unknown species" : speciesName.Trim();
        MinimumCount = minimumCount;
        EnemyFactory = enemyFactory ?? throw new ArgumentNullException(nameof(enemyFactory));
        ReactionStrategyFactory = reactionStrategyFactory ?? throw new ArgumentNullException(nameof(reactionStrategyFactory));
        MovementStrategyFactory = movementStrategyFactory ?? throw new ArgumentNullException(nameof(movementStrategyFactory));
    }

    public string SpeciesName { get; }

    public int MinimumCount { get; }

    public Func<Enemy> EnemyFactory { get; }

    public Func<ISpeciesReactionStrategy> ReactionStrategyFactory { get; }

    public Func<IEnemyMovementStrategy> MovementStrategyFactory { get; }
}
