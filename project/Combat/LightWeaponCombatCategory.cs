using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public sealed class LightWeaponCombatCategory : IWeaponCombatCategory
{
    public string Name => "Light";

    public int CalculateAttackDamage(Weapon weapon, Player player, IAttackStyle attackStyle)
    {
        return weapon.GetDamageValue();
    }

    public int CalculateDefenseStrength(Weapon weapon, Player player, IAttackStyle attackStyle)
    {
        return weapon.GetDefenseValue() + player.Stats.Dexterity;
    }
}
