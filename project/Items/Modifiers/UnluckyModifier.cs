namespace ConsoleRpgStage1.Items.Modifiers;

public sealed class UnluckyModifier : WeaponModifier
{
    public UnluckyModifier(Weapon innerWeapon) : base(innerWeapon, "Unlucky")
    {
    }

    public override int GetLuckModifier() => InnerWeapon.GetLuckModifier() - 5;
}
