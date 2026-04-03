using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.Items.Modifiers;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddWeaponsProcedure : IDungeonBuildProcedure
{
    private readonly int _count;
    private readonly Func<Weapon>[] _weaponFactories;
    private readonly Random _random;

    public AddWeaponsProcedure(int count, Random? random = null)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Weapon count cannot be negative.");
        }

        _count = count;
        _random = random ?? Random.Shared;
        _weaponFactories =
        [
            static () => new ShortSwordItem(),
            static () => new BattleAxeItem(),
            static () => new GreatswordItem(),
            static () => new WizardStaffItem()
        ];
    }

    public void Apply(World world)
    {
        if (_count == 0)
        {
            return;
        }

        var walkablePositions = DungeonPlacementHelper.GetWalkablePositions(world);
        if (walkablePositions.Count == 0)
        {
            return;
        }

        DungeonPlacementHelper.Shuffle(walkablePositions, _random);

        var placements = Math.Min(_count, walkablePositions.Count);
        for (var index = 0; index < placements; index++)
        {
            var position = walkablePositions[index];
            var weapon = CreateRandomWeapon();
            world.AddItem(position, weapon);
        }
    }

    private Item CreateRandomWeapon()
    {
        var factory = _weaponFactories[_random.Next(_weaponFactories.Length)];
        Weapon weapon = factory();

        var modifierAppliers = new Func<Weapon, Weapon>[]
        {
            static innerWeapon => new StrongModifier(innerWeapon),
            static innerWeapon => new UnluckyWeaponModifier(innerWeapon)
        };

        DungeonPlacementHelper.Shuffle(modifierAppliers, _random);

        var modifiersToApply = _random.Next(0, modifierAppliers.Length + 1);
        for (var index = 0; index < modifiersToApply; index++)
        {
            weapon = modifierAppliers[index](weapon);
        }

        return weapon;
    }
}
