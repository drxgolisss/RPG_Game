namespace ConsoleRpgStage1.Items.Modifiers;

public sealed class UnluckyModifier : ItemModifier
{
    public UnluckyModifier(Item innerItem) : base(innerItem, "Unlucky")
    {
    }

    public override int GetLuckModifier() => InnerItem.GetLuckModifier() - 5;
}
