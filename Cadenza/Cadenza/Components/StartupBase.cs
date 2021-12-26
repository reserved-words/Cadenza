namespace Cadenza.Components;

public class StartupBase : ComponentBase
{
    [Inject]
    public MudBlazor.IDialogService DialogService { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public IStartupSyncService SyncService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var syncTasks = SyncService.GetLibrarySyncTasks();

        var dialogParameters = new DialogParameters();
        dialogParameters.Add(nameof(ProgressDialog.TaskGroup), syncTasks);
        //dialogParameters.Add(nameof(ProgressDialog.AutoStart), false);
        dialogParameters.Add(nameof(ProgressDialog.StartPromptText), "Would you like to re-sync source libraries?");

        var dialogReference = DialogService.Show<ProgressDialog>("Syncing Library", dialogParameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        });

        var result = await dialogReference.Result;

        if (!result.Cancelled)
        {
            App.Initialise();
        }
    }
}
