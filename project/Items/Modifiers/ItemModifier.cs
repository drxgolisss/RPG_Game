namespace ConsoleRpgStage1.Items.Modifiers;

public abstract class ItemModifier : Item
{
    protected ItemModifier(Item innerItem, string modifierName)
        : base(GetInnerItem(innerItem).Name, innerItem.Symbol)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifierName);

        InnerItem = innerItem;
        ModifierName = modifierName;
    }

    protected Item InnerItem { get; }

    protected string ModifierName { get; }

    public override string Name => $"{InnerItem.Name} ({ModifierName})";

    public override char Symbol => InnerItem.Symbol;

    public override bool CanEquip => InnerItem.CanEquip;

    public override void OnPickedUp(Entities.Player player)
    {
        InnerItem.HandlePickup(player, this);
    }

    public override Entities.EquipResult TryEquip(Entities.Player player, Entities.Hand hand)
    {
        return InnerItem.TryEquip(player, hand);
    }

    public override int GetLuckModifier()
    {
        return InnerItem.GetLuckModifier();
    }

    private static Item GetInnerItem(Item innerItem)
    {
        ArgumentNullException.ThrowIfNull(innerItem);
        return innerItem;
    }
}
