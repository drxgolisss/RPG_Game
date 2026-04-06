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

    public abstract int CalculateAttackDamageForHeavyWeapon(Weapon weapon, Player player);

    public abstract int CalculateAttackDamageForLightWeapon(Weapon weapon, Player player);

    public abstract int CalculateAttackDamageForMagicalWeapon(Weapon weapon, Player player);

    public abstract int CalculateDefenseStrengthWithoutWeapon(Player player);

    public abstract int CalculateDefenseStrengthForHeavyWeapon(Weapon weapon, Player player);

    public abstract int CalculateDefenseStrengthForLightWeapon(Weapon weapon, Player player);

    public abstract int CalculateDefenseStrengthForMagicalWeapon(Weapon weapon, Player player);

    protected static int GetEffectiveLuck(Player player)
    {
        return player.Stats.Luck + player.GetLuckModifier();
    }
}
