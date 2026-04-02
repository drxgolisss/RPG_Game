namespace ConsoleRpgStage1.Items;

public sealed class BattleAxeItem : Weapon
{
    public BattleAxeItem() : base("Battle Axe", ')', 11, HandRequirement.OneHanded, new ConsoleRpgStage1.Combat.HeavyWeaponCombatCategory())
    {
    }
}
