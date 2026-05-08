using ConsoleRpgStage1.Combat;

namespace ConsoleRpgStage1.Items;

public sealed class BlackWandItem : Weapon
{
    public BlackWandItem() : base(
        "Black Wand",
        'w',
        10,
        HandRequirement.OneHanded,
        new MagicalWeaponCombatCategory(),
        defense: 1)
    {
    }
}
