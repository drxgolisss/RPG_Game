namespace ConsoleRpgStage1.Items;

public sealed class WizardStaffItem : Weapon
{
    public WizardStaffItem() : base(
        "Wizard Staff",
        '|',
        9,
        HandRequirement.TwoHanded,
        new ConsoleRpgStage1.Combat.MagicalWeaponCombatCategory(),
        defense: 1)
    {
    }
}
