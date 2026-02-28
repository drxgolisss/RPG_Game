using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Entities;

public sealed class Equipment
{
    private Weapon? _leftItem;
    private Weapon? _rightItem;

    public Weapon? LeftItem => _leftItem;

    public Weapon? RightItem => _rightItem;

    public EquipResult TryEquip(Weapon weapon, Hand hand)
    {
        return hand.TryEquip(this, weapon);
    }

    public EquipResult TryUnequip(Player player, Hand hand)
    {
        return hand.TryUnequip(player, this);
    }

    public EquipResult TryEquipLeft(Weapon weapon)
    {
        if (weapon.HandsRequired == 2)
        {
            return TryEquipTwoHanded(weapon);
        }

        if (HasTwoHandedEquipped())
        {
            return EquipResult.Fail("Cannot equip one-handed weapon: both hands are occupied by two-handed weapon.");
        }

        if (_leftItem != null)
        {
            return EquipResult.Fail("Left hand is already occupied.");
        }

        _leftItem = weapon;
        return EquipResult.Success($"Equipped {weapon.Name} in left hand.");
    }

    public EquipResult TryEquipRight(Weapon weapon)
    {
        if (weapon.HandsRequired == 2)
        {
            return TryEquipTwoHanded(weapon);
        }

        if (HasTwoHandedEquipped())
        {
            return EquipResult.Fail("Cannot equip one-handed weapon: both hands are occupied by two-handed weapon.");
        }

        if (_rightItem != null)
        {
            return EquipResult.Fail("Right hand is already occupied.");
        }

        _rightItem = weapon;
        return EquipResult.Success($"Equipped {weapon.Name} in right hand.");
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
            return EquipResult.Success($"Unequipped two-handed weapon {removedItem.Name}.");
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
            return EquipResult.Success($"Unequipped two-handed weapon {removedItem.Name}.");
        }

        _rightItem = null;
        player.AddToInventory(removedItem);
        return EquipResult.Success($"Unequipped {removedItem.Name} from right hand.");
    }

    private EquipResult TryEquipTwoHanded(Weapon weapon)
    {
        if (_leftItem != null || _rightItem != null)
        {
            return EquipResult.Fail("Cannot equip two-handed weapon: hands are not free.");
        }

        _leftItem = weapon;
        _rightItem = weapon;
        return EquipResult.Success($"Equipped two-handed weapon {weapon.Name}.");
    }

    private bool HasTwoHandedEquipped()
    {
        return _leftItem != null &&
               _rightItem != null &&
               ReferenceEquals(_leftItem, _rightItem) &&
               _leftItem.HandsRequired == 2;
    }
}
