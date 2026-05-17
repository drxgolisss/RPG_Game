using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Logging;
using ConsoleRpgStage1.Reactive.Movement;
using ConsoleRpgStage1.Reactive.Notifications;
using ConsoleRpgStage1.Reactive.Sound;
using ConsoleRpgStage1.Reactive.Species;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Entities;

public sealed class Enemy : INoiseObserver, IDeathObserver
{
    private IEnemyMovementStrategy _movementStrategy;
    private ISoundPropagation _soundPropagation;

    public Enemy(int health, int attack, int armor, string name = "Enemy", char symbol = 'E')
    {
        if (health <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(health), "Health must be greater than zero.");
        }

        if (attack < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(attack), "Attack cannot be negative.");
        }

        if (armor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(armor), "Armor cannot be negative.");
        }

        Health = health;
        Attack = attack;
        Armor = armor;
        Name = string.IsNullOrWhiteSpace(name) ? "Enemy" : name.Trim();
        Symbol = symbol;
        Position = new Position(0, 0);
        _movementStrategy = new RandomWalkMovementStrategy();
        _soundPropagation = new SoundPropagationService();
    }

    public string Name { get; }

    public int Health { get; private set; }

    public int Attack { get; private set; }

    public int Armor { get; private set; }

    public Position Position { get; private set; }

    public char Symbol { get; }

    public EnemySpeciesGroup? SpeciesGroup { get; private set; }

    public bool IsDead => Health <= 0;

    public void ApplyDamage(int damage)
    {
        Health = Math.Max(0, Health - Math.Max(0, damage));
    }

    public void ApplyStatChange(int attackDelta, int armorDelta)
    {
        Attack = Math.Max(0, Attack + attackDelta);
        Armor = Math.Max(0, Armor + armorDelta);
    }

    public void SetPosition(Position position)
    {
        Position = position;
    }

    public void SetMovementStrategy(IEnemyMovementStrategy movementStrategy)
    {
        _movementStrategy = movementStrategy ?? throw new ArgumentNullException(nameof(movementStrategy));
    }

    public void SetSoundPropagation(ISoundPropagation soundPropagation)
    {
        _soundPropagation = soundPropagation ?? throw new ArgumentNullException(nameof(soundPropagation));
    }

    public void AssignSpeciesGroup(EnemySpeciesGroup speciesGroup)
    {
        SpeciesGroup = speciesGroup ?? throw new ArgumentNullException(nameof(speciesGroup));
    }

    public void Move(GameWorld world)
    {
        _movementStrategy.Move(this, world);
    }

    public void OnNoise(NoiseEvent noiseEvent)
    {
        ArgumentNullException.ThrowIfNull(noiseEvent);

        var heardDistance = _soundPropagation.GetDistance(
            noiseEvent.World,
            noiseEvent.Source,
            Position,
            noiseEvent.Range);

        if (!heardDistance.HasValue)
        {
            return;
        }

        GameLogger.Instance.AddEntry(
            $"{Name} at ({Position.Row},{Position.Col}) heard noise from ({noiseEvent.Source.Row},{noiseEvent.Source.Col}) at distance {heardDistance.Value}: {noiseEvent.Description}.");
    }

    public void OnDeath(EnemyDeathEvent deathEvent)
    {
        ArgumentNullException.ThrowIfNull(deathEvent);
        SpeciesGroup?.ReactToDeath(this, deathEvent);
    }

    public void BroadcastDeath()
    {
        SpeciesGroup?.NotifyMemberDeath(this);
    }

    public void DetachFromSpecies()
    {
        SpeciesGroup?.RemoveMember(this);
        SpeciesGroup = null;
    }
}
