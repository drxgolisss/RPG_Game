using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public sealed class StealthAttackStyle : AttackStyleBase
{
    public override string Name => "Stealth";

    public override int CalculateAttackDamageForHeavyWeapon(Weapon weapon, Player player)
    {
        return GetHeavyWeaponDamage(weapon, player) / 2;
    }

    public override int CalculateAttackDamageForLightWeapon(Weapon weapon, Player player)
    {
        return GetLightWeaponDamage(weapon, player) * 2;
    }

    public override int CalculateAttackDamageForMagicalWeapon(Weapon weapon, Player player)
    {
        return 1;
    }

    public override int CalculateDefenseStrengthWithoutWeapon(Player player)
    {
        return 0;
    }

    public override int CalculateDefenseStrengthForOtherItem(Item item, Player player)
    {
        return 0;
    }

    public override int CalculateDefenseStrengthForHeavyWeapon(Weapon weapon, Player player)
    {
        return WithWeaponDefense(weapon, player.Stats.Strength);
    }

    public override int CalculateDefenseStrengthForLightWeapon(Weapon weapon, Player player)
    {
        return WithWeaponDefense(weapon, player.Stats.Dexterity);
    }

    public override int CalculateDefenseStrengthForMagicalWeapon(Weapon weapon, Player player)
    {
        return WithWeaponDefense(weapon, 0);
    }
}
