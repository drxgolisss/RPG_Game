using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items;

public abstract class Item
{
    protected Item(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }

    public string Name { get; }

    public char Symbol { get; }

    public virtual bool CanEquip => false;

    public virtual void OnPickedUp(Player player)
    {
        player.AddToInventory(this);
    }

    public virtual EquipResult TryEquip(Player player, Hand hand)
    {
        return EquipResult.Fail($"Cannot equip {Name}.");
    }

    public override string ToString() => $"{Name} ({Symbol})";
}
