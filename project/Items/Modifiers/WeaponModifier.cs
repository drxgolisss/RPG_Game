using ConsoleRpgStage1.Combat;
using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items.Modifiers;

public abstract class WeaponModifier : Weapon
{
    protected WeaponModifier(Weapon innerWeapon, string modifierName)
        : base(
            GetInnerWeapon(innerWeapon).Name,
            innerWeapon.Symbol,
            innerWeapon.Damage,
            innerWeapon.HandRequirement,
            innerWeapon.CombatCategory,
            innerWeapon.Defense)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifierName);

        InnerWeapon = innerWeapon;
        ModifierName = modifierName;
    }

    protected Weapon InnerWeapon { get; }

    protected string ModifierName { get; }

    public override string Name => $"{InnerWeapon.Name} ({ModifierName})";

    public override char Symbol => InnerWeapon.Symbol;

    public override int GetDamageValue() => InnerWeapon.GetDamageValue();

    public override int GetDefenseValue() => InnerWeapon.GetDefenseValue();

    public override int GetAttackDamage(Player player, IAttackStyle attackStyle)
    {
        return CombatCategory.CalculateAttackDamage(this, player, attackStyle);
    }

    public override int GetDefenseStrength(Player player, IAttackStyle attackStyle)
    {
        return CombatCategory.CalculateDefenseStrength(this, player, attackStyle);
    }

    public override int GetLuckModifier()
    {
        return InnerWeapon.GetLuckModifier();
    }

    private static Weapon GetInnerWeapon(Weapon innerWeapon)
    {
        ArgumentNullException.ThrowIfNull(innerWeapon);
        return innerWeapon;
    }
}
