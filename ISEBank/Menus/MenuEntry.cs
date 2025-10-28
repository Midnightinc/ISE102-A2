namespace ISEBank.Menus;


public abstract class MenuEntry
{
    public bool handling = true;

    public MenuEntry(string shortName, string description, Func<bool> conditionalDisplay)
    {
            this.ShortName = shortName;
            this.Description = description;
            ConditionalDisplay = conditionalDisplay;
    }

    public string ShortName { get; protected set; }
    public string Description { get; protected set; }
    public MenuEntry? Parent { get; protected internal set; } = null;

    public Func<bool> ConditionalDisplay { get; }

    public void Select()
    {
        OnSelect();
    }
    
    protected abstract void OnSelect();
}


public class ActionMenuEntry : MenuEntry
{
    private readonly Action<MenuEntry> _action;
    public ActionMenuEntry(string shortName, string description, Action<MenuEntry> action, Func<bool> conditionalDisplay = null) : base(shortName, description, conditionalDisplay)
    {
        _action = action;
    }

    protected override void OnSelect()
    {
        _action.Invoke(this);
    }
}

public class SubMenuEntry : MenuEntry
{
    private List<MenuEntry> Entries { get; } = new List<MenuEntry>();
    public SubMenuEntry(string shortName, string description, Func<bool> conditionalDisplay = null) : base(shortName, description, conditionalDisplay)
    {
    }

    public void AddEntry(MenuEntry entry)
    {
        Entries.Add(entry);
        entry.Parent = this;
    }

    protected override void OnSelect()
    {
        while (handling && !App.ShouldExit)
        {
            Console.Clear();
            Console.WriteLine($"--- {Description} ---\n");

            
            foreach (var child in Entries)
            {
                if (child.ConditionalDisplay != null)
                {
                    if (child.ConditionalDisplay())
                    {
                        Console.WriteLine($"({child.ShortName}) {child.Description}");
                    }
                }
                else
                {
                    Console.WriteLine($"({child.ShortName}) {child.Description}");
                }
            }

            if (Parent != null)
            {
                Console.WriteLine($"\n(back) Return to previous menu.");
            }
            
            Console.Write("\nSelect an option: ");

            var input = Console.ReadLine()?.Trim().ToLower();

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