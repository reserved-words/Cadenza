namespace Cadenza.Components;

public class StartupBase : ComponentBase
{
    [Inject]
    public MudBlazor.IDialogService DialogService { get; set; }

    [Inject]
    public IAppController App { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var dialogReference = DialogService.Show<StartupSyncDialog>("Syncing Library", new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        });

        var result = await dialogReference.Result;

        if (!result.Cancelled)
        {
            await App.Initialise();
        }
    }
}
