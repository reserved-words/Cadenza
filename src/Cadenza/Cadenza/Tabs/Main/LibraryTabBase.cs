using Cadenza.Web.Common.Interfaces.Startup;
using Cadenza.Web.Components.Shared;

namespace Cadenza;

public class LibraryTabBase : ComponentBase
{
    [Inject]
    public IStartupConnectService ConnectService { get; set; }

    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    protected bool IsInitalised { get; private set; }

    public List<DynamicTabsItem> FixedItems = new();

    public List<DynamicTabsItem> DynamicItems = new();

    public string SelectedItem { get; set; }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<ViewItemEventArgs>(OnViewItem);
    }

    private Task OnViewItem(object sender, ViewItemEventArgs e)
    {
        if (!DynamicItems.Any(t => t.Id == e.Item.Id))
        {
            DynamicItems.Add(GetItemTab(e));
        }

        SelectedItem = e.Item.Id;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static DynamicTabsItem GetItemTab(ViewItemEventArgs e)
    {
        return new DynamicTabsItem(e.Item.Id, e.Item.Name, e.Item.Type.GetIcon(), typeof(ItemTab), new Dictionary<string, object>
        {
            { "Item", e.Item },
        });
    }
}
