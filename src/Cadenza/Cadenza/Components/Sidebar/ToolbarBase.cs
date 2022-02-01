
namespace Cadenza;

public class ToolbarBase : ComponentBase
{
    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IAppController AppController { get; set; }

    [Inject]
    public ISyncService SyncService { get; set; }

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
        var completed = await DialogService.Run(() => SyncService.GetLibrarySyncTasks(), "Sync Library", false, "Would you like to re-sync source libraries?");

        if (completed)
        {
        }
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