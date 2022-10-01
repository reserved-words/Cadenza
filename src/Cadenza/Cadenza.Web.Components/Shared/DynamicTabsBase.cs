namespace Cadenza.Web.Components.Shared;

public class DynamicTabsBase : ComponentBase
{
    protected bool IsInitalised { get; private set; }

    [Parameter]
    public List<DynamicTabsItem> FixedItems { get; set; } = new();

    [Parameter]
    public List<DynamicTabsItem> DynamicItems { get; set; } = new();

    [Parameter]
    public string SelectedItem { get; set; }

    public int SelectedTabIndex { get; set; }

    private IEnumerable<DynamicTabsItem> _allItems => FixedItems.Concat(DynamicItems);

    protected void CloseTabCallback(MudTabPanel panel)
    {
        var itemId = panel.Tag.ToString();
        if (DynamicItems.Any(i => i.Id == itemId))
        {
            var tab = DynamicItems.Single(i => i.Id == itemId);
            DynamicItems.Remove(tab);
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (SelectedItem != null)
        {
            var allItems = _allItems.ToList();

            var item = allItems.SingleOrDefault(i => i.Id == SelectedItem);
            if (item != null)
            {
                SelectedTabIndex = allItems.IndexOf(item);
                StateHasChanged();
            }

            SelectedItem = null;
        }
    }
}
