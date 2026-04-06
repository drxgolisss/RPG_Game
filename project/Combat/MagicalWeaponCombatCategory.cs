using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public sealed class MagicalWeaponCombatCategory : IWeaponCombatCategory
{
    public string Name => "Magical";

    public int CalculateAttackDamage(Weapon weapon, Player player, IAttackStyle attackStyle)
    {
        return attackStyle.CalculateAttackDamageForMagicalWeapon(weapon, player);
    }

    public int CalculateDefenseStrength(Weapon weapon, Player player, IAttackStyle attackStyle)
    {
        return attackStyle.CalculateDefenseStrengthForMagicalWeapon(weapon, player);
    }
}
