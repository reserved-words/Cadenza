namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public IStartupConnectService ConnectService { get; set; }

    [Inject]
    public IProgressDialogService DialogService { get; set; }

    public bool IsInitalised { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await OnStartup();
    }

    protected async Task OnStartup()
    {
        var success = await DialogService.Run(() => ConnectService.GetStartupTasks(), "Connecting Services", true);
        IsInitalised = success;
    }
}