using Cadenza.Components.Dialogs;
using IDialogService = MudBlazor.IDialogService;

namespace Cadenza.Services;

internal class MudStartupDialogService : IStartupDialogService
{
    private readonly IDialogService _dialogService;

    public MudStartupDialogService(IDialogService dialogService)
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

        return !result.Canceled;
    }
}
