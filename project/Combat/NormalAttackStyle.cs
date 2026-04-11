using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public sealed class NormalAttackStyle : AttackStyleBase
{
    public override string Name => "Normal";

    public override int CalculateAttackDamageForHeavyWeapon(Weapon weapon, Player player)
    {
        return GetHeavyWeaponDamage(weapon, player);
    }

    public override int CalculateAttackDamageForLightWeapon(Weapon weapon, Player player)
    {
        return GetLightWeaponDamage(weapon, player);
    }

    public override int CalculateAttackDamageForMagicalWeapon(Weapon weapon, Player player)
    {
        return 1;
    }

    public override int CalculateDefenseStrengthWithoutWeapon(Player player)
    {
        return Math.Max(0, player.Stats.Dexterity);
    }

    public override int CalculateDefenseStrengthForOtherItem(Item item, Player player)
    {
        return Math.Max(0, player.Stats.Dexterity);
    }

    public override int CalculateDefenseStrengthForHeavyWeapon(Weapon weapon, Player player)
    {
        return WithWeaponDefense(weapon, player.Stats.Strength + GetEffectiveLuck(player));
    }

    public override int CalculateDefenseStrengthForLightWeapon(Weapon weapon, Player player)
    {
        return WithWeaponDefense(weapon, player.Stats.Dexterity + GetEffectiveLuck(player));
    }

    public override int CalculateDefenseStrengthForMagicalWeapon(Weapon weapon, Player player)
    {
        return WithWeaponDefense(weapon, player.Stats.Dexterity + GetEffectiveLuck(player));
    }
}
