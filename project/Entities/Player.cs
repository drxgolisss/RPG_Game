using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Entities;

public sealed class Player
{
    private readonly List<Item> _inventory = new();

    public Player(Position startPosition)
    {
        Position = startPosition;
        LeftHand = new LeftHand();
        RightHand = new RightHand();
        Equipment = new Equipment();
    }

    public IReadOnlyList<Item> Inventory => _inventory;

    public Position Position { get; private set; }

    public int Coins { get; private set; }

    public int Gold { get; private set; }

    public Hand LeftHand { get; }

    public Hand RightHand { get; }

    public Equipment Equipment { get; }

    public bool TryMove(Direction direction, World.World world)
    {
        var newPosition = new Position(
            Position.Row + direction.DeltaRow,
            Position.Col + direction.DeltaCol);

        if (!world.CanEnter(newPosition))
        {
            return false;
        }

        Position = newPosition;
        return true;
    }

    public bool TryPickUp(World.World world)
    {
        var cell = world.GetCell(Position);
        if (!cell.TryTakeFirstItem(out var item))
        {
            return false;
        }

        item.OnPickedUp(this);
        return true;
    }

    public bool TryDropItem(int index, World.World world, out string message)
    {
        if (!IsValidInventoryIndex(index))
        {
            message = "Invalid inventory index.";
            return false;
        }

        var item = _inventory[index];
        _inventory.RemoveAt(index);
        world.AddItem(Position, item);

        message = $"Dropped {item.Name}.";
        return true;
    }

    public EquipResult TryEquipItem(int index, Hand hand)
    {
        if (!IsValidInventoryIndex(index))
        {
            return EquipResult.Fail("Invalid inventory index.");
        }

        var item = _inventory[index];
        var result = item.TryEquip(this, hand);

        if (result.IsSuccess)
        {
            _inventory.RemoveAt(index);
        }

        return result;
    }

    public EquipResult TryUnequip(Hand hand)
    {
        return Equipment.TryUnequip(this, hand);
    }

    public void AddToInventory(Item item)
    {
        _inventory.Add(item);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    private bool IsValidInventoryIndex(int index)
    {
        return index >= 0 && index < _inventory.Count;
    }
}
