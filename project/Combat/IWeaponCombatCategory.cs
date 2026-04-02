using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public interface IWeaponCombatCategory
{
    string Name { get; }

    int CalculateAttackDamage(Weapon weapon, Player player, IAttackStyle attackStyle);

    int CalculateDefenseStrength(Weapon weapon, Player player, IAttackStyle attackStyle);
}
