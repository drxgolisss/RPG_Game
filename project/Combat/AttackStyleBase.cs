using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public abstract class AttackStyleBase : IAttackStyle
{
    public abstract string Name { get; }

    public virtual int CalculateAttackDamageWithoutWeapon(Player player)
    {
        return 0;
    }

    public virtual int CalculateAttackDamageForOtherItem(Item item, Player player)
    {
        return 0;
    }

    public abstract int CalculateAttackDamageForHeavyWeapon(Weapon weapon, Player player);

    public abstract int CalculateAttackDamageForLightWeapon(Weapon weapon, Player player);

    public abstract int CalculateAttackDamageForMagicalWeapon(Weapon weapon, Player player);

    public abstract int CalculateDefenseStrengthWithoutWeapon(Player player);

    public virtual int CalculateDefenseStrengthForOtherItem(Item item, Player player)
    {
        return CalculateDefenseStrengthWithoutWeapon(player);
    }

    public abstract int CalculateDefenseStrengthForHeavyWeapon(Weapon weapon, Player player);

    public abstract int CalculateDefenseStrengthForLightWeapon(Weapon weapon, Player player);

    public abstract int CalculateDefenseStrengthForMagicalWeapon(Weapon weapon, Player player);

    protected static int GetHeavyWeaponDamage(Weapon weapon, Player player)
    {
        return Math.Max(0, weapon.GetDamageValue() + player.Stats.Strength + player.Stats.Aggression);
    }

    protected static int GetLightWeaponDamage(Weapon weapon, Player player)
    {
        return Math.Max(0, weapon.GetDamageValue() + player.Stats.Dexterity + GetEffectiveLuck(player));
    }

    protected static int GetMagicalWeaponDamage(Weapon weapon, Player player)
    {
        return Math.Max(0, weapon.GetDamageValue() + player.Stats.Wisdom);
    }

    protected static int WithWeaponDefense(Weapon weapon, int defense)
    {
        return Math.Max(0, weapon.GetDefenseValue() + defense);
    }

    protected static int GetEffectiveLuck(Player player)
    {
        return player.Stats.Luck + player.GetLuckModifier();
    }
}
