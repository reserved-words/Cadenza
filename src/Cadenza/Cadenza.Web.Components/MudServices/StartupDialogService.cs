namespace Cadenza.Web.Components.MudServices;

internal class StartupDialogService : IStartupDialogService
{
    private readonly IDialogService _dialogService;

    public StartupDialogService(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> Run(List<ConnectionStartupParameter> connections)
    {
        var dialogParameters = new DialogParameters
        {
            { nameof(StartupDialog.Connections), connections }
        };

        var dialogReference = _dialogService.Show<StartupDialog>("Starting application", dialogParameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        });

        var result = await dialogReference.Result;

        return !result.Cancelled;
    }
}
