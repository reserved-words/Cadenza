
namespace Cadenza;

public class ToolbarBase : ComponentBase
{
    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IAppController AppController { get; set; }

    [Inject]
    public IStartupConnectService ConnectService { get; set; }

    [Inject]
    public IStartupSyncService SyncService { get; set; }

    [Inject]
    public IStoreSetter StoreSetter { get; set; }

    [Inject]
    public IConnectorController ConnectorService { get; set; }

    public List<ConnectorStatusViewModel> ConnectorStatuses { get; set; }

    protected override void OnInitialized()
    {
        ConnectorStatuses = Enum.GetValues<Connector>()
            .Select(c => new ConnectorStatusViewModel
            {
                Connector = c,
                Status = ConnectorStatus.Loading
            })
            .ToList();
    }

    protected async Task OnSync()
    {
        var success = await DialogService.Run(() => SyncService.GetLibrarySyncTasks(), "Sync Library", false, "Would you like to re-sync source libraries?");
    }

    protected async Task OnClearSession()
    {
        var keys = Enum.GetValues<StoreKey>().ToList();

        foreach (var key in keys)
        {
            await StoreSetter.Clear(key);
        }

        await ConnectorService.SetStatus(Connector.LastFm, ConnectorStatus.Disabled);
        await ConnectorService.SetStatus(Connector.Spotify, ConnectorStatus.Disabled);

        var success = await DialogService.Run(() => ConnectService.GetStartupTasks(), "Connect Services", false, "Reconnect services?");
    }
}

public class ConnectorStatusViewModel
{
    public Connector Connector { get; set; }
    public ConnectorStatus Status { get; set; }
    public string ErrorTitle { get; set; }
    public string ErrorMessage { get; set; }
    public bool ShowError { get; set; }
}