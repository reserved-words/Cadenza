namespace Cadenza.UI.Shared;

public class DynamicTabsItem
{
    public DynamicTabsItem(string id, string title, string icon, Type component, Dictionary<string, object> parameters = null)
    {
        Id = id;
        Title = title;
        Icon = icon;
        RenderFragment = component.RenderFragment(parameters);
    }

    public string Id { get; }
    public string Icon { get; }
    public string Title { get; }
    public RenderFragment RenderFragment { get; }
}
