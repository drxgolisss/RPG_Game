using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public sealed class HeavyWeaponCombatCategory : IWeaponCombatCategory
{
    public string Name => "Heavy";

    public int CalculateAttackDamage(Weapon weapon, Player player, IAttackStyle attackStyle)
    {
        return attackStyle.CalculateAttackDamageForHeavyWeapon(weapon, player);
    }

    public int CalculateDefenseStrength(Weapon weapon, Player player, IAttackStyle attackStyle)
    {
        return attackStyle.CalculateDefenseStrengthForHeavyWeapon(weapon, player);
    }
}
