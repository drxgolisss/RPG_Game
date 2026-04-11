using ConsoleRpgStage1.Combat;
using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items;

public abstract class Weapon : Item
{
    protected Weapon(
        string name,
        char symbol,
        int damage,
        HandRequirement handsRequired,
        IWeaponCombatCategory combatCategory,
        int defense = 0)
        : base(name, symbol)
    {
        ArgumentNullException.ThrowIfNull(combatCategory);

        Damage = damage;
        HandRequirement = handsRequired;
        CombatCategory = combatCategory;
        Defense = defense;
    }

    public int Damage { get; }

    public int Defense { get; }

    public HandRequirement HandRequirement { get; }

    public IWeaponCombatCategory CombatCategory { get; }

    public override bool CanEquip => true;

    public override bool OccupiesBothHands => HandRequirement == HandRequirement.TwoHanded;

    public virtual int GetDamageValue() => Damage;

    public virtual int GetDefenseValue() => Defense;

    public override int GetAttackDamage(Player player, IAttackStyle attackStyle)
    {
        return CombatCategory.CalculateAttackDamage(this, player, attackStyle);
    }

    public override int GetDefenseStrength(Player player, IAttackStyle attackStyle)
    {
        return CombatCategory.CalculateDefenseStrength(this, player, attackStyle);
    }

    public override EquipResult TryEquip(Player player, Hand hand)
    {
        return player.Equipment.TryEquip(this, hand);
    }
}
