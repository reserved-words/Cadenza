namespace Cadenza;

public class ProgressDialogService : IProgressDialogService
{
    private readonly MudBlazor.IDialogService _dialogService;

    public ProgressDialogService(MudBlazor.IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> Run(TaskGroup tasks, bool autoStart, string startPromptText = null)
    {
        var dialogParameters = new DialogParameters();
        dialogParameters.Add(nameof(ProgressDialog.TaskGroup), tasks);
        dialogParameters.Add(nameof(ProgressDialog.AutoStart), autoStart);
        dialogParameters.Add(nameof(ProgressDialog.StartPromptText), "Would you like to re-sync source libraries?");

        var dialogReference = _dialogService.Show<ProgressDialog>("Syncing Library", dialogParameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        });

        var result = await dialogReference.Result;

        return !result.Cancelled;
    }
}
