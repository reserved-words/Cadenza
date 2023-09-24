namespace Cadenza.Web.Components.MudServices;

internal class MudProgressDialogService : IProgressDialogService
{
    private readonly IDialogService _dialogService;

    public MudProgressDialogService(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> Run(Func<TaskGroup> taskGroupFactory, string title)
    {
        var dialogParameters = new DialogParameters
        {
            { nameof(ProgressDialog.TaskGroupFactory), taskGroupFactory }
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
