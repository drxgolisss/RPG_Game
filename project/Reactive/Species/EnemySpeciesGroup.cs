using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Reactive.Notifications;

namespace ConsoleRpgStage1.Reactive.Species;

public sealed class EnemySpeciesGroup
{
    private readonly DeathBroadcaster _deathBroadcaster = new();
    private readonly List<Enemy> _livingMembers = new();
    private readonly ISpeciesReactionStrategy _reactionStrategy;

    public EnemySpeciesGroup(string name, ISpeciesReactionStrategy reactionStrategy)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Unknown species" : name.Trim();
        _reactionStrategy = reactionStrategy ?? throw new ArgumentNullException(nameof(reactionStrategy));
    }

    public string Name { get; }

    public IReadOnlyList<Enemy> LivingMembers => _livingMembers;

    public void AddMember(Enemy enemy)
    {
        ArgumentNullException.ThrowIfNull(enemy);

        if (_livingMembers.Contains(enemy))
        {
            return;
        }

        _livingMembers.Add(enemy);
        enemy.AssignSpeciesGroup(this);
        _deathBroadcaster.Subscribe(enemy);
    }

    public void RemoveMember(Enemy enemy)
    {
        ArgumentNullException.ThrowIfNull(enemy);
        _livingMembers.Remove(enemy);
        _deathBroadcaster.Unsubscribe(enemy);
    }

    public void NotifyMemberDeath(Enemy deadEnemy)
    {
        ArgumentNullException.ThrowIfNull(deadEnemy);

        var deathEvent = new EnemyDeathEvent(deadEnemy, this, deadEnemy.Position);
        _deathBroadcaster.NotifyDeath(deathEvent);
    }

    public void ReactToDeath(Enemy survivor, EnemyDeathEvent deathEvent)
    {
        if (ReferenceEquals(survivor, deathEvent.DeadEnemy))
        {
            return;
        }

        _reactionStrategy.React(survivor, deathEvent);
    }
}
