using Cadenza.Core.App;
using Cadenza.Core.CurrentlyPlaying;
using Cadenza.Core.Model;

namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public IStartupConnectService ConnectService { get; set; }

    [Inject]
    public IStartupSyncService SyncService { get; set; }

    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    public bool Playing { get; set; }

    public int FixedTabCount => Playing ? 3 : 2;

    protected bool IsInitalised { get; private set; }

    protected int SelectedTabIndex = 0;
    
    private string SwitchToTab = null;

    protected List<ViewItem> ItemTabs = new();

    protected override async Task OnInitializedAsync()
    {
        App.TrackStarted += App_TrackStarted;
        App.ItemRequested += App_ItemRequested;

        await OnStartup();
    }

    private Task App_ItemRequested(object sender, ItemEventArgs e)
    {
        if (ItemTabs.Any(t => t.Id == e.Item.Id))
        {
            ShowTab(e.Item.Id);
        }
        else
        {
            ItemTabs.Add(e.Item);
            SwitchToTab = e.Item.Id;
        }

        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task App_TrackStarted(object sender, TrackEventArgs e)
    {
        Playing = true;
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected async Task OnStartup()
    {
        var success = await DialogService.Run(() => ConnectService.GetStartupTasks(), "Connecting Services", true);

        if (success)
        {
            success = await DialogService.Run(() => SyncService.GetStartupTasks(), "Sync Library", true);
        }

        IsInitalised = success;
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
        if (SwitchToTab != null)
        {
            ShowTab(SwitchToTab);
            SwitchToTab = null;
        } 
    }

    private void ShowTab(string id)
    {
        var tab = ItemTabs.Single(t => t.Id == id);
        var index = ItemTabs.IndexOf(tab);
        var newIndex = FixedTabCount + index;

        if (SelectedTabIndex == newIndex)
        {
            // Display message
        }
        else
        {
            SelectedTabIndex = FixedTabCount + index;
        }

        StateHasChanged();
    }
}