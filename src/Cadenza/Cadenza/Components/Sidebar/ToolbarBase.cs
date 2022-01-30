
namespace Cadenza;

public class ToolbarBase : ComponentBase
{
    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public INotificationService Notification { get; set; }

    [Inject]
    public IAppConsumer AppConsumer { get; set; }

    [Inject]
    public IAppController AppController { get; set; }

    [Inject]
    public ISyncService SyncService { get; set; }

    public List<ConnectorStatus> ConnectorStatuses { get; set; } = new List<ConnectorStatus>();

    protected override void OnInitialized()
    {
        ConnectorStatuses.Add(new ConnectorStatus { Connector = Connector.LastFm, Enabled = true, Loading = true });
        ConnectorStatuses.Add(new ConnectorStatus { Connector = Connector.Local, Enabled = true, Loading = false });
        ConnectorStatuses.Add(new ConnectorStatus { Connector = Connector.Spotify, Enabled = true, Loading = true });

        AppConsumer.ConnectorDisabled += OnConnectorDisabled;
        AppConsumer.ConnectorEnabled += OnConnectorEnabled;
    }

    private Task OnConnectorEnabled(object sender, ConnectorEventArgs e)
    {
        var status = ConnectorStatuses.Single(s => s.Connector == e.Connector);
        status.MarkAsEnabled();
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected async Task OnSync()
    {
        var completed = await DialogService.Run(() => SyncService.GetLibrarySyncTasks(), false, "Would you like to re-sync source libraries?");

        if (completed)
        {
            AppController.Initialise();
        }
    }

    private Task OnConnectorDisabled(object sender, ConnectorEventArgs e)
    {
        var status = ConnectorStatuses.Single(s => s.Connector == e.Connector);

        if (status.ErrorMessage != e.Error)
        {
            Notification.Error($"{e.Connector} error: {e.Error}");
        }

        status.MarkAsErrored(e.Error);
        StateHasChanged();
        return Task.CompletedTask;
    }
}

public class ConnectorStatus
{
    public Connector Connector { get; set; }
    public bool Loading { get; set; }
    public bool Enabled { get; set; }
    public string ErrorTitle { get; set; }
    public string ErrorMessage { get; set; }
    public bool ShowError { get; set; }

    public void MarkAsEnabled()
    {
        Loading = false;
        Enabled = true;
        ErrorTitle = null;
        ErrorMessage = null;
        ShowError = false;
    }

    public void MarkAsErrored(string error)
    {
        Loading = false;
        Enabled = false;
        ErrorTitle = $"{Connector} disabled";
        ErrorMessage = error;
    }
}