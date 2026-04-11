namespace ConsoleRpgStage1.Items;

public abstract class JunkItem : Item
{
    protected JunkItem(string name, char symbol) : base(name, symbol)
    {
    }

    public override bool CanEquip => true;

    public override Entities.EquipResult TryEquip(Entities.Player player, Entities.Hand hand)
    {
        return player.Equipment.TryEquip(this, hand);
    }
}
