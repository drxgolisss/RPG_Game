using ConsoleRpgStage1.Logging;

namespace ConsoleRpgStage1.UI;

public sealed class EventLogScreen
{
    public void Show()
    {
        Console.Clear();
        Console.WriteLine("Event log");
        Console.WriteLine();

        var entries = GameLogger.Instance.GetAllEntries();
        if (entries.Count == 0)
        {
            Console.WriteLine("(no entries yet)");
        }
        else
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return.");
        Console.ReadKey(true);
        Console.Clear();
    }
}
