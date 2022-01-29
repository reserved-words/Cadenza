using Cadenza.Core;

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
        SourceStatuses.Add(new SourceStatus { Source = LibrarySource.Local, Enabled = true });
        SourceStatuses.Add(new SourceStatus { Source = LibrarySource.Spotify, Enabled = true });

        AppConsumer.SourceErrored += OnSourceErrored;
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

        status.Enabled = false;
        status.ErrorTitle = $"{e.Source} disabled";
        status.ErrorMessage = e.Error;

        StateHasChanged();
        return Task.CompletedTask;
    }

    protected void HideSourceError(SourceStatus sourceStatus)
    {
        sourceStatus.ShowError = false;
        StateHasChanged();
    }

    protected void ShowSourceError(SourceStatus sourceStatus)
    {
        sourceStatus.ShowError = true;
        StateHasChanged();
    }
}

public class SourceStatus
{
    public LibrarySource Source { get; set; }
    public bool Enabled { get; set; }
    public string ErrorTitle { get; set; }
    public string ErrorMessage { get; set; }
    public bool ShowError { get; set; }
}