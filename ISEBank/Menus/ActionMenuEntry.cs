namespace ISEBank.Menus;

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