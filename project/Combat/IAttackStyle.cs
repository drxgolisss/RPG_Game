using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public interface IAttackStyle
{
    string Name { get; }

    int CalculateAttackDamageWithoutWeapon(Player player);

    int CalculateAttackDamageForHeavyWeapon(Weapon weapon, Player player);

    int CalculateAttackDamageForLightWeapon(Weapon weapon, Player player);

    int CalculateAttackDamageForMagicalWeapon(Weapon weapon, Player player);

    int CalculateDefenseStrengthWithoutWeapon(Player player);

    int CalculateDefenseStrengthForHeavyWeapon(Weapon weapon, Player player);

    int CalculateDefenseStrengthForLightWeapon(Weapon weapon, Player player);

    int CalculateDefenseStrengthForMagicalWeapon(Weapon weapon, Player player);
}
