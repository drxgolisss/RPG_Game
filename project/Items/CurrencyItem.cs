using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Items;

public abstract class CurrencyItem : Item
{
    protected CurrencyItem(string name, char symbol, int amount) : base(name, symbol)
    {
        Amount = amount;
    }

    public int Amount { get; }

    public sealed override void OnPickedUp(Player player)
    {
        Deposit(player);
    }

    protected abstract void Deposit(Player player);
}
