namespace ConsoleRpgStage1.Items.Modifiers;

public abstract class ItemModifier : Item
{
    protected ItemModifier(Item innerItem, string modifierName)
        : base(innerItem.Name, innerItem.Symbol)
    {
        ArgumentNullException.ThrowIfNull(innerItem);
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
        InnerItem.OnPickedUp(player);
    }

    public override Entities.EquipResult TryEquip(Entities.Player player, Entities.Hand hand)
    {
        return InnerItem.TryEquip(player, hand);
    }

    public override int GetLuckModifier()
    {
        return InnerItem.GetLuckModifier();
    }
}
