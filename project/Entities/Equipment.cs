using ConsoleRpgStage1.Items;
namespace ConsoleRpgStage1.Entities;

public sealed class Equipment
{
    private Item? _leftItem;
    private Item? _rightItem;

    public Item? LeftItem => _leftItem;

    public Item? RightItem => _rightItem;

    public EquipResult TryEquip(Item item, Hand hand)
    {
        return hand.TryEquip(this, item);
    }

    public EquipResult TryUnequip(Player player, Hand hand)
    {
        return hand.TryUnequip(player, this);
    }

    public EquipResult TryEquipLeft(Item item)
    {
        if (item.OccupiesBothHands)
        {
            return TryEquipTwoHanded(item);
        }

        if (HasTwoHandedEquipped())
        {
            return EquipResult.Fail("Cannot equip item: both hands are occupied by two-handed item.");
        }

        if (_leftItem != null)
        {
            return EquipResult.Fail("Left hand is already occupied.");
        }

        _leftItem = item;
        return EquipResult.Success($"Equipped {item.Name} in left hand.");
    }

    public EquipResult TryEquipRight(Item item)
    {
        if (item.OccupiesBothHands)
        {
            return TryEquipTwoHanded(item);
        }

        if (HasTwoHandedEquipped())
        {
            return EquipResult.Fail("Cannot equip item: both hands are occupied by two-handed item.");
        }

        if (_rightItem != null)
        {
            return EquipResult.Fail("Right hand is already occupied.");
        }

        _rightItem = item;
        return EquipResult.Success($"Equipped {item.Name} in right hand.");
    }

    public EquipResult TryUnequipLeft(Player player)
    {
        if (_leftItem == null)
        {
            return EquipResult.Fail("Left hand is already empty.");
        }

        var removedItem = _leftItem;
        if (HasTwoHandedEquipped())
        {
            _leftItem = null;
            _rightItem = null;
            player.AddToInventory(removedItem);
            return EquipResult.Success($"Unequipped two-handed item {removedItem.Name}.");
        }

        _leftItem = null;
        player.AddToInventory(removedItem);
        return EquipResult.Success($"Unequipped {removedItem.Name} from left hand.");
    }

    public EquipResult TryUnequipRight(Player player)
    {
        if (_rightItem == null)
        {
            return EquipResult.Fail("Right hand is already empty.");
        }

        var removedItem = _rightItem;
        if (HasTwoHandedEquipped())
        {
            _leftItem = null;
            _rightItem = null;
            player.AddToInventory(removedItem);
            return EquipResult.Success($"Unequipped two-handed item {removedItem.Name}.");
        }

        _rightItem = null;
        player.AddToInventory(removedItem);
        return EquipResult.Success($"Unequipped {removedItem.Name} from right hand.");
    }

    private EquipResult TryEquipTwoHanded(Item item)
    {
        if (_leftItem != null || _rightItem != null)
        {
            return EquipResult.Fail("Cannot equip two-handed item: hands are not free.");
        }

        _leftItem = item;
        _rightItem = item;
        return EquipResult.Success($"Equipped two-handed item {item.Name}.");
    }

    private bool HasTwoHandedEquipped()
    {
        return _leftItem != null &&
               _rightItem != null &&
               ReferenceEquals(_leftItem, _rightItem) &&
               (_leftItem.OccupiesBothHands || _rightItem.OccupiesBothHands);
    }
}
