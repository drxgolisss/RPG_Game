using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items;

public sealed class ItemGold : CurrencyItem
{
    public ItemGold() : base("Gold", '$', 1)
    {
    }

    protected override void Deposit(Player player)
    {
        player.AddGold(Amount);
    }
}
