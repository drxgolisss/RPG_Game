namespace ConsoleRpgStage1.Items.Modifiers;

public sealed class UnluckyWeaponModifier : WeaponModifier
{
    public UnluckyWeaponModifier(Weapon innerWeapon) : base(innerWeapon, "Unlucky")
    {
    }

    public override int GetLuckModifier() => InnerWeapon.GetLuckModifier() - 5;
}
