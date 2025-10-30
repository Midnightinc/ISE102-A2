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