using Cadenza.Interfaces;
using Cadenza.UI.Shared;
using Cadenza.UI.Tabs.Main;
using Cadenza.Web.Core.App;
using Cadenza.Web.Core.Interfaces;

namespace Cadenza.Pages;

public class IndexBase : ComponentBase
{
    [Inject]
    public IStartupConnectService ConnectService { get; set; }

    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    protected bool IsInitalised { get; private set; }

    public List<DynamicTabsItem> FixedItems = new();

    public List<DynamicTabsItem> DynamicItems = new();

    public string SelectedItem { get; set; }

    protected override async Task OnInitializedAsync()
    {
        FixedItems = new List<DynamicTabsItem>
        {
            new DynamicTabsItem("Home", null, "fas fa-history", typeof(HistoryTab)),
            new DynamicTabsItem("Playing", null, "fas fa-volume-up", typeof(CurrentlyPlayingTab)),
            //new DynamicTabsItem("System", null, "fas fa-cog", typeof(SystemInfoTab))
        };

        App.ItemRequested += App_ItemRequested;

        await OnStartup();
    }

    private Task App_ItemRequested(object sender, ItemEventArgs e)
    {
        if (!DynamicItems.Any(t => t.Id == e.Item.Id))
        {
            DynamicItems.Add(GetItemTab(e));
        }

        SelectedItem = e.Item.Id;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static DynamicTabsItem GetItemTab(ItemEventArgs e)
    {
        return new DynamicTabsItem(e.Item.Id, e.Item.Name, e.Item.Type.GetIcon(), typeof(ItemTab), new Dictionary<string, object>
        {
            { "Item", e.Item },
        });
    }

    protected async Task OnStartup()
    {
        var success = await DialogService.Run(() => ConnectService.GetStartupTasks(), "Connecting Services", true);
        IsInitalised = success;
    }
}