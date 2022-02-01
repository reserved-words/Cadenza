using Cadenza.Common;
using Microsoft.JSInterop;

namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public IProgressDialogService DialogService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IStoreSetter Store { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IConfiguration Config { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public IConnectorController ConnectorController { get; set; }

    [Inject]
    public Cadenza.API.Core.LastFM.IAuthoriser LastFmAuthoriser { get; set; }

    public bool IsInitalised { get; private set; }

    private Uri CurrentUri => NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

    private string RedirectUri => Config.GetSection("LastFm").GetValue<string>("RedirectUri");

    protected override async Task OnInitializedAsync()
    {
        await OnStartup();
    }

    private Task OnConnectorInitialised(object sender, ConnectorEventArgs e)
    {
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected async Task OnStartup()
    {
        var completed = await DialogService.Run(() => GetStartupTasks(), "Connecting Services", true);
    }

    private TaskGroup GetStartupTasks()
    {
        var taskGroup = new TaskGroup
        {
            PreTask = ClearSessionData
        };

        taskGroup.Tasks.Add(GetLastFmConnectTask());

        return taskGroup;
    }

    private async Task ClearSessionData()
    {
        await Store.SetValue(StoreKey.CurrentTrackSource, null);
        await Store.SetValue(StoreKey.CurrentTrack, null);
        await Store.SetValue(StoreKey.LastFmSessionKey, null);
        await Store.SetValue(StoreKey.SpotifyAccessToken, null);
        await Store.SetValue(StoreKey.SpotifyRefreshToken, null);
        await Store.SetValue(StoreKey.SpotifyDeviceId, null);
    }

    private SubTask GetLastFmConnectTask()
    {
        var subTask = new SubTask
        {
            Id = "LastFM",
            Title = "Connect to Last.FM",
            Steps = new List<TaskStep>(),
            OnError = (ex) => ConnectorController.SetStatus(Connector.LastFm, ConnectorStatus.Errored, ex.Message)
        };

        subTask.AddSteps(
            "Get auth URL",
            "Navigating to auth URL",
            () => LastFmAuthoriser.GetAuthUrl(RedirectUri),
            (url) => NavigateToNewTab(url));

        return subTask;
    }

    public async Task NavigateToNewTab(string url)
    {
        await JSRuntime.InvokeAsync<object>("open", url, "_blank");
    }
}