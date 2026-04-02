namespace ConsoleRpgStage1.Items.Modifiers;

public sealed class StrongModifier : WeaponModifier
{
    public StrongModifier(Weapon innerWeapon) : base(innerWeapon, "Strong")
    {
    }

    public override int GetDamageValue() => InnerWeapon.GetDamageValue() + 5;
}
