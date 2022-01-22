namespace Cadenza;

public class SyncBase : ComponentBase
{
    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public IStartupSyncService SyncService { get; set; }

    protected async Task OnSync()
    {
        var syncTasks = SyncService.GetLibrarySyncTasks();

        var completed = await DialogService.Run(syncTasks, false, "Would you like to re-sync source libraries?");

        if (completed)
        {
            App.Initialise();
        }
    }
}