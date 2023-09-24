namespace Cadenza.Web.Components.MudServices;

internal class MudProgressDialogService : IProgressDialogService
{
    private readonly IDialogService _dialogService;

    public MudProgressDialogService(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> Run(Func<List<SubTask>> taskFactory)
    {
        var dialogParameters = new DialogParameters
        {
            { nameof(ProgressDialog.TaskFactory), taskFactory }
        };

        var dialogReference = _dialogService.Show<ProgressDialog>("Starting application", dialogParameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        });

        var result = await dialogReference.Result;

        return !result.Cancelled;
    }
}
