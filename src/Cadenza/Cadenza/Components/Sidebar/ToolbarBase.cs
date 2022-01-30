
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

    public List<SourceStatus> SourceStatuses { get; set; } = new List<SourceStatus>();

    protected override void OnInitialized()
    {
        SourceStatuses.Add(new SourceStatus { Source = LibrarySource.Local, Enabled = true, Loading = false });
        SourceStatuses.Add(new SourceStatus { Source = LibrarySource.Spotify, Enabled = true, Loading = true });

        AppConsumer.SourceErrored += OnSourceErrored;
        AppConsumer.SourceEnabled += OnSourceEnabled;
    }

    private Task OnSourceEnabled(object sender, SourceEventArgs e)
    {
        var status = SourceStatuses.Single(s => s.Source == e.Source);
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

    private Task OnSourceErrored(object sender, SourceEventArgs e)
    {
        var status = SourceStatuses.Single(s => s.Source == e.Source);

        if (status.ErrorMessage != e.Error)
        {
            Notification.Error($"{e.Source} error: {e.Error}");
        }

        status.MarkAsErrored(e.Error);
        StateHasChanged();
        return Task.CompletedTask;
    }
}

public class SourceStatus
{
    public LibrarySource Source { get; set; }
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
        ErrorTitle = $"{Source} disabled";
        ErrorMessage = error;
    }
}