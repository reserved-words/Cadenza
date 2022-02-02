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
    public IStoreGetter StoreGetter { get; set; }

    [Inject]
    public IStoreSetter StoreSetter { get; set; }

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
        // First step is clearing session data, but maybe instead should check the updated date and
        // only clear if older than a certain amount of time
        // Then check for existing values before doing any of the startup stuff

        await DialogService.Run(() => GetStartupTasks(), "Connecting Services", true);

        // Do this as one of the final steps instead of waiting, could also add a loop to wait a fixed amount of time

        var lastFmToken = await StoreGetter.GetValue<string>(StoreKey.LastFmToken);

        if (lastFmToken == null)
        {
            // Display something asking user to confirm when they've granted access
        }
        else
        {
            var sessionKey = await LastFmAuthoriser.CreateSession(lastFmToken.Value);
            await StoreSetter.SetValue(StoreKey.LastFmSessionKey, sessionKey);
        }
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
        await StoreSetter.Clear(StoreKey.CurrentTrackSource);
        await StoreSetter.Clear(StoreKey.CurrentTrack);
        await StoreSetter.Clear(StoreKey.LastFmSessionKey);
        await StoreSetter.Clear(StoreKey.LastFmToken);
        await StoreSetter.Clear(StoreKey.SpotifyAccessToken);
        await StoreSetter.Clear(StoreKey.SpotifyRefreshToken);
        await StoreSetter.Clear(StoreKey.SpotifyDeviceId);
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
        await JSRuntime.InvokeVoidAsync("open", url, "_blank");
    }
}