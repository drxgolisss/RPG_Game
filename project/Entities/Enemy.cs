using ConsoleRpgStage1.Core;

namespace ConsoleRpgStage1.Entities;

public sealed class Enemy
{
    public Enemy(int health, int attack, int armor)
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
        Position = new Position(0, 0);
    }

    public int Health { get; private set; }

    public int Attack { get; }

    public int Armor { get; }

    public Position Position { get; private set; }

    public bool IsDead => Health <= 0;

    public void ApplyDamage(int damage)
    {
        Health = Math.Max(0, Health - Math.Max(0, damage));
    }

    public void SetPosition(Position position)
    {
        Position = position;
    }
}
