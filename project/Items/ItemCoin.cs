using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items;

public sealed class ItemCoin : CurrencyItem
{
    public ItemCoin() : base("Coin", 'c', 1)
    {
    }

    protected override void Deposit(Player player)
    {
        player.AddCoins(Amount);
    }
}
