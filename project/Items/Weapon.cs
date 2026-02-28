using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items;

public abstract class Weapon : Item
{
    protected Weapon(string name, char symbol, int damage, int handsRequired)
        : base(name, symbol)
    {
        Damage = damage;
        HandsRequired = handsRequired;
    }

    public int Damage { get; }

    public int HandsRequired { get; }

    public override bool CanEquip => true;

    public override EquipResult TryEquip(Player player, Hand hand)
    {
        return player.Equipment.TryEquip(this, hand);
    }
}
