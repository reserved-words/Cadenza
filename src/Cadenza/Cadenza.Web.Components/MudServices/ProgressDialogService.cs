namespace Cadenza.Web.Components.MudServices;

public class ProgressDialogService : IProgressDialogService
{
    private readonly MudBlazor.IDialogService _dialogService;

    public ProgressDialogService(MudBlazor.IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> Run(Func<TaskGroup> taskGroupFactory, string title, bool autoStart, string startPromptText = null)
    {
        var dialogParameters = new DialogParameters
        {
            { nameof(ProgressDialog.TaskGroupFactory), taskGroupFactory },
            { nameof(ProgressDialog.AutoStart), autoStart },
            { nameof(ProgressDialog.StartPromptText), startPromptText }
        };

        var dialogReference = _dialogService.Show<ProgressDialog>(title, dialogParameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        });

        var result = await dialogReference.Result;

        return !result.Cancelled;
    }
}
