using ConsoleRpgStage1.Combat;

namespace ConsoleRpgStage1.Items;

public sealed class BlasterItem : Weapon
{
    public BlasterItem() : base(
        "Blaster",
        '}',
        12,
        HandRequirement.OneHanded,
        new LightWeaponCombatCategory())
    {
    }
}
