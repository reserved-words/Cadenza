using Cadenza.Common;
using Cadenza.Components.Shared.Dialogs;
using Cadenza.Core;

namespace Cadenza;

public class ProgressDialogService : IProgressDialogService
{
    private readonly MudBlazor.IDialogService _dialogService;

    public ProgressDialogService(MudBlazor.IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> Run(Func<TaskGroup> taskGroupFactory, bool autoStart, string startPromptText = null)
    {
        var dialogParameters = new DialogParameters();
        dialogParameters.Add(nameof(ProgressDialog.TaskGroupFactory), taskGroupFactory);
        dialogParameters.Add(nameof(ProgressDialog.AutoStart), autoStart);
        dialogParameters.Add(nameof(ProgressDialog.StartPromptText), startPromptText);

        var dialogReference = _dialogService.Show<ProgressDialog>("Sync Library", dialogParameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        });

        var result = await dialogReference.Result;

        return !result.Cancelled;
    }
}
