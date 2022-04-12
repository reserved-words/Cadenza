using Cadenza.Components.Shared;
using Cadenza.Components.Tabs;
using Cadenza.Core.App;
using Cadenza.Core.CurrentlyPlaying;
using Cadenza.Core.Model;
using Cadenza.Interfaces;
using Microsoft.AspNetCore.Components.Rendering;

namespace Cadenza.Pages;

public class IndexBase : ComponentBase
{
    [Inject]
    public IStartupConnectService ConnectService { get; set; }

    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    public bool Playing { get; set; }

    protected bool IsInitalised { get; private set; }

    protected List<ViewItem> ItemTabs = new(); 
    
    public List<DynamicTabsItem> FixedItems = new();

    public List<DynamicTabsItem> DynamicItems = new();

    public string SelectedItem { get; set; }

    protected override async Task OnInitializedAsync()
    {
        FixedItems = new List<DynamicTabsItem>
        {
            new DynamicTabsItem("Home", "Home", "fas fa-history", typeof(HistoryTab)),
            new DynamicTabsItem("Playing", "Playing", "fas fa-volume-up", typeof(CurrentlyPlayingTab)),
            new DynamicTabsItem("Spotify", "Spotify", Icon.Spotify, typeof(SpotifyTab)),
            new DynamicTabsItem("System", "System", "fas fa-cog", typeof(SystemInfoTab))
        };

        App.TrackStarted += App_TrackStarted;
        App.ItemRequested += App_ItemRequested;

        await OnStartup();
    }

    private Task App_ItemRequested(object sender, ItemEventArgs e)
    {
        if (!DynamicItems.Any(t => t.Id == e.Item.Id))
        {
             DynamicItems.Add(new DynamicTabsItem(e.Item.Id, e.Item.Name, e.Item.Type.GetIcon(), typeof(ItemTab), new Dictionary<string, object>
             {
                 { "Item", e.Item },
             }));
        }

        SelectedItem = e.Item.Id;

        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task App_TrackStarted(object sender, TrackEventArgs e)
    {
        //Playing = true;
        //StateHasChanged();
        return Task.CompletedTask;
    }

    protected async Task OnStartup()
    {
        var success = await DialogService.Run(() => ConnectService.GetStartupTasks(), "Connecting Services", true);

        IsInitalised = success;
    }

    //protected void CloseTabCallback(MudTabPanel panel)
    //{
    //    var itemId = panel.Tag.ToString();
    //    if (ItemTabs.Any(i => i.Id == itemId))
    //    {
    //        var tab = ItemTabs.Single(i => i.Id == itemId);
    //        ItemTabs.Remove(tab);
    //    }
    //}

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
    //    var tab = ItemTabs.Single(t => t.Id == id);
    //    var index = ItemTabs.IndexOf(tab);
    //    var newIndex = FixedTabCount + index;

    //    if (SelectedTabIndex == newIndex)
    //    {
    //        // Display message
    //    }
    //    else
    //    {
    //        SelectedTabIndex = FixedTabCount + index;
    //    }

    //    StateHasChanged();
    //}
}