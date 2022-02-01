
namespace Cadenza;

public class ToolbarBase : ComponentBase
{
    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public INotificationService Notification { get; set; }

    [Inject]
    public IConnectorConsumer ConnectorService { get; set; }

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

        ConnectorService.ConnectorStatusChanged += OnConnectorStatusChanged;
    }

    protected async Task OnSync()
    {
        var completed = await DialogService.Run(() => SyncService.GetLibrarySyncTasks(), false, "Would you like to re-sync source libraries?");

        if (completed)
        {
            AppController.Initialise();
        }
    }

    private Task OnConnectorStatusChanged(object sender, ConnectorEventArgs e)
    {
        var model = ConnectorStatuses.Single(s => s.Connector == e.Connector);
        model.UpdateStatus(e.Status, e.Error);

        if (e.Status == ConnectorStatus.Errored)
        {
            Notification.Error($"{e.Connector} error: {e.Error}");
        }

        StateHasChanged();
        return Task.CompletedTask;
    }
}

public class ConnectorStatusViewModel
{
    public Connector Connector { get; set; }
    public ConnectorStatus Status { get; set; }
    public string ErrorTitle { get; set; }
    public string ErrorMessage { get; set; }
    public bool ShowError { get; set; }

    public void UpdateStatus(ConnectorStatus status, string error)
    {
        Status = status;
        ErrorTitle = status == ConnectorStatus.Errored 
            ? $"{Connector} errored" 
            : null;
        ErrorMessage = status == ConnectorStatus.Errored
            ? error
            : null;
        ShowError = false;
    }
}