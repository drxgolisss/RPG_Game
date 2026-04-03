using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items;

public abstract class Item
{
    private readonly string _name;
    private readonly char _symbol;

    protected Item(string name, char symbol)
    {
        _name = name;
        _symbol = symbol;
    }

    public virtual string Name => _name;

    public virtual char Symbol => _symbol;

    public virtual bool CanEquip => false;

    public virtual int GetLuckModifier() => 0;

    public virtual void OnPickedUp(Player player)
    {
        HandlePickup(player, this);
    }

    public virtual EquipResult TryEquip(Player player, Hand hand)
    {
        return EquipResult.Fail($"Cannot equip {Name}.");
    }

    internal virtual void HandlePickup(Player player, Item pickedUpItem)
    {
        player.AddToInventory(pickedUpItem);
    }

    public override string ToString() => $"{Name} ({Symbol})";
}
