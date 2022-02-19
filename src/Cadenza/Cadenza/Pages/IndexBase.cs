using Cadenza.Components.Sidebar;

namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public IStartupConnectService ConnectService { get; set; }

    [Inject]
    public IStartupSyncService SyncService { get; set; }

    [Inject]
    public IProgressDialogService DialogService { get; set; }

    protected bool IsInitalised { get; private set; }

    protected int SelectedTabIndex = 0;
    
    //protected bool UpdateIndex = false;

    protected List<SearchResultItem> ItemTabs = new();

    protected override async Task OnInitializedAsync()
    {
        await OnStartup();
    }

    protected async Task OnStartup()
    {
        //IsInitalised = true;

        var success = await DialogService.Run(() => ConnectService.GetStartupTasks(), "Connecting Services", true);

        if (success)
        {
            success = await DialogService.Run(() => SyncService.GetStartupTasks(), "Sync Library", true);
        }

        IsInitalised = success;
    }

    protected async Task OnViewItem(SearchResultItem item)
    {
        ItemTabs.Add(item);

        StateHasChanged();
    }

    protected void AddTabCallback()
    {
        //_tabs.Add(new TabView { Name = "Dynamic Content", Content = "A new tab", Id = Guid.NewGuid().ToString() });
        //the tab becomes available after it is rendered. Hence, we can't set the index here
        //UpdateIndex = true;
    }

    protected void CloseTabCallback(MudTabPanel panel)
    {
        var itemId = panel.Tag.ToString();
        if (ItemTabs.Any(i => i.Id == itemId))
        {
            var tab = ItemTabs.Single(i => i.Id == itemId);
            ItemTabs.Remove(tab);
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        //if (UpdateIndex == true)
        //{
        //    SelectedTabIndex = (4 + ItemTabs.Count) - 1;
        //    StateHasChanged();
        //    UpdateIndex = false;
        //}
    }
}