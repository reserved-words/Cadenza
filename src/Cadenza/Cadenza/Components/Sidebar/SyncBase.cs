using Cadenza.Core;

namespace Cadenza;

public class SyncBase : ComponentBase
{
    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public ISyncService SyncService { get; set; }

    protected async Task OnSync()
    {
        var completed = await DialogService.Run(() => SyncService.GetLibrarySyncTasks(), false, "Would you like to re-sync source libraries?");

        if (completed)
        {
            App.Initialise();
        }
    }
}