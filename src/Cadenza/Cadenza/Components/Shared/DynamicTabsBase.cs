namespace Cadenza.Components.Shared;

public class DynamicTabsBase : ComponentBase
{
    protected bool IsInitalised { get; private set; }

    [Parameter]
    public List<DynamicTabsItem> FixedItems { get; set; } = new();

    [Parameter]
    public List<DynamicTabsItem> DynamicItems { get; set; } = new();

    public int SelectedTabIndex { get; set; }

    //private void AddTab(DynamicTabsItem tab)
    //{
    //    if (DynamicItems.Any(t => t.Id == tab.Id))
    //    {
    //        ShowTab(tab.Id);
    //    }
    //    else
    //    {
    //        DynamicItems.Add(tab);
    //        SwitchToTab = tab.Id;
    //    }

    //    StateHasChanged();
    //}

    protected void CloseTabCallback(MudTabPanel panel)
    {
        var itemId = panel.Tag.ToString();
        if (DynamicItems.Any(i => i.Id == itemId))
        {
            var tab = DynamicItems.Single(i => i.Id == itemId);
            DynamicItems.Remove(tab);
        }
    }

    //protected override void OnAfterRender(bool firstRender)
    //{
    //    if (SwitchToTab != null)
    //    {
    //        ShowTab(SwitchToTab);
    //        SwitchToTab = null;
    //    }
    //}

    //private void ShowTab(string id)
    //{
    //    var tab = DynamicItems.Single(t => t.Id == id);
    //    var index = DynamicItems.IndexOf(tab);
    //    var newIndex = FixedItems.Count + index;

    //    if (SelectedTabIndex != newIndex)
    //    {
    //        SelectedTabIndex = newIndex;
    //    }

    //    StateHasChanged();
    //}
}

public struct DynamicTabsItem
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
