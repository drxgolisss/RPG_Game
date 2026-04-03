using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.Items.Modifiers;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddItemsProcedure : IDungeonBuildProcedure
{
    private readonly int _count;
    private readonly Func<Item>[] _itemFactories;
    private readonly Random _random;

    public AddItemsProcedure(int count, Random? random = null)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Item count cannot be negative.");
        }

        _count = count;
        _random = random ?? Random.Shared;
        _itemFactories =
        [
            static () => new ItemRock(),
            static () => new BrokenBottleItem(),
            static () => new RustyGearItem()
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
            var item = CreateRandomItem();
            world.AddItem(position, item);
        }
    }

    private Item CreateRandomItem()
    {
        var factory = _itemFactories[_random.Next(_itemFactories.Length)];
        var item = factory();

        if (_random.Next(0, 2) == 0)
        {
            return item;
        }

        return new UnluckyModifier(item);
    }
}
