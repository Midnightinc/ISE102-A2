namespace ISEBank.Menus;

public class Menu
{
    public Menu(string shortName, string description)
    {
        //ShortName = shortName;
        //Description = description;
    }

    public List<MenuEntry> Entries { get; } = [];
    
    private Dictionary<string, MenuEntry> Options { get; } = new Dictionary<string, MenuEntry>();
    
    
    
    public void ShowEntries()
    {
        BuildValidOptions();
        foreach (var entry in Options)
        {
            Console.WriteLine($"{entry.Key}.  {entry.Value.ShortName}");
        }
    }

    public void SelectOption(string key)
    {
        if (Options.ContainsKey(key))
        {
            Options[key].Select();
        }
    }

    private void BuildValidOptions()
    {
    }
}