using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddMandatoryItemProcedure : IDungeonBuildProcedure
{
    private readonly Func<Item> _itemFactory;
    private readonly Random _random;

    public AddMandatoryItemProcedure(Func<Item> itemFactory, Random? random = null)
    {
        _itemFactory = itemFactory ?? throw new ArgumentNullException(nameof(itemFactory));
        _random = random ?? Random.Shared;
    }

    public void Apply(World world)
    {
        var walkablePositions = DungeonPlacementHelper.GetWalkablePositions(world);
        if (walkablePositions.Count == 0)
        {
            return;
        }

        var position = walkablePositions[_random.Next(walkablePositions.Count)];
        world.AddItem(position, _itemFactory());
    }
}
