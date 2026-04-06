using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public sealed class MagicalAttackStyle : AttackStyleBase
{
    public override string Name => "Magical";

    public override int CalculateAttackDamageForHeavyWeapon(Weapon weapon, Player player)
    {
        return 1;
    }

    public override int CalculateAttackDamageForLightWeapon(Weapon weapon, Player player)
    {
        return 1;
    }

    public override int CalculateAttackDamageForMagicalWeapon(Weapon weapon, Player player)
    {
        return weapon.GetDamageValue();
    }

    public override int CalculateDefenseStrengthWithoutWeapon(Player player)
    {
        return Math.Max(0, GetEffectiveLuck(player));
    }

    public override int CalculateDefenseStrengthForHeavyWeapon(Weapon weapon, Player player)
    {
        return Math.Max(0, GetEffectiveLuck(player));
    }

    public override int CalculateDefenseStrengthForLightWeapon(Weapon weapon, Player player)
    {
        return Math.Max(0, GetEffectiveLuck(player));
    }

    public override int CalculateDefenseStrengthForMagicalWeapon(Weapon weapon, Player player)
    {
        return Math.Max(0, player.Stats.Wisdom * 2);
    }
}
