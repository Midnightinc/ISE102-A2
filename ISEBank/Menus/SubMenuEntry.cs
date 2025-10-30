namespace ISEBank.Menus;

public class SubMenuEntry : MenuEntry
{
    public List<MenuEntry> Entries { get; } = new List<MenuEntry>();
    private int selectedIndex = 0;
    private Dictionary<int, MenuEntry> ValidOptions = new Dictionary<int, MenuEntry>();
    public SubMenuEntry(string shortName, string description, Func<bool> conditionalDisplay = null) : base(shortName, description, conditionalDisplay)
    {
    }

    public void AddEntry(MenuEntry entry)
    {
        Entries.Add(entry);
        entry.Parent = this;
    }

    public void ValidateMenuStack()
    {
        foreach (var menuEntry in Entries)
        {
            var subMenuEntry = menuEntry as SubMenuEntry;
            if (subMenuEntry != null)
            {
                subMenuEntry.Parent = this;
                subMenuEntry.ValidateMenuStack();
            }
        }
    }

    protected override void OnSelect()
    {
        while (handling && !App.ShouldExit)
        {
            Console.Clear();
            Console.WriteLine($"--- {Description} ---\n");

            int i = 0;
            ValidOptions.Clear();
            foreach (var child in Entries)
            {
                if (child.ConditionalDisplay != null)
                {
                    if (!child.ConditionalDisplay())
                    {
                        continue;
                    }
                }
                Console.Write(selectedIndex == i ? "-> " : "   ");
                Console.WriteLine($"({child.ShortName}) {child.Description}");
                ValidOptions.Add(i, child);
                i++;
            }

            if (Parent != null)
            {
                if (selectedIndex == Entries.Count)
                {
                    Console.WriteLine($"-> Return to previous menu.");
                }
                else
                {
                    Console.WriteLine($"   Return to previous menu.");
                }
            }
            
            //Console.Write("\nSelect an option: ");
            
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex--;
                    selectedIndex = Math.Clamp(selectedIndex, 0, Entries.Count);
                    continue;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex++;
                    selectedIndex = Math.Clamp(selectedIndex, 0, Entries.Count);
                    continue;
                    break;
                case ConsoleKey.Enter:
                    if (selectedIndex == Entries.Count)
                    {
                        handling = false;
                    }
                    else
                    {
                        var selectedEntry = ValidOptions[selectedIndex];
                        selectedEntry.Select();
                        if (selectedEntry is not ActionMenuEntry) continue;
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }
                    continue;
                    break;
                case ConsoleKey.Escape:
                    Console.WriteLine("Escaped");
                    handling = false;
                    continue;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.ReadKey();
                    continue;
                    break;
            }

            //var input = Console.ReadLine()?.Trim().ToLower();
            var input = "";

            if (input == "back" && Parent != null)
            {
                handling = false;
            }
            else
            {
                
                var selectedEntry = Entries.FirstOrDefault(e => e.ShortName.ToLower() == input);

                if (selectedEntry != null)
                {
                    selectedEntry.Select();
                    
                    // If the child was an ACTION, pause the screen
                    // so the user can see the result before the menu re-draws.
                    if (selectedEntry is not ActionMenuEntry) continue;
                    Console.WriteLine("\nPress any key to continue...");
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }

                Console.ReadKey();
            }
        }
    }
}