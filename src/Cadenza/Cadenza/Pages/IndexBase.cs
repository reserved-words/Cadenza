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
        await DialogService.Run(() => GetStartupTasks(), "Connecting Services", true);
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
        // Temporary while testing startup
        //await StoreSetter.Clear(StoreKey.LastFmToken);
        //await StoreSetter.Clear(StoreKey.LastFmSessionKey);


        await StoreSetter.Clear(StoreKey.CurrentTrackSource);
        await StoreSetter.Clear(StoreKey.CurrentTrack);
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
            CheckStep = new TaskCheckStep { Caption = "Checking if Last.FM already connected", Task = IsLastFmTaskNeeded },
            Steps = new List<TaskStep>(),
            OnError = (ex) => ConnectorController.SetStatus(Connector.LastFm, ConnectorStatus.Errored, ex.Message),
            OnCompleted = () => ConnectorController.SetStatus(Connector.LastFm, ConnectorStatus.Connected)
        };

        // Add step to check is SK is present and not timed out
        subTask.AddSteps(
            "Getting auth URL",
            "Saving session key",
            () => GetAuthUrl(),
            (sk) => SaveSessionKey(sk),
            ("Authenticating", (url) => Authorise(url)),
            ("Creating session", (token) => CreateSession(token)));

        return subTask;
    }

    public async Task NavigateToNewTab(string url)
    {
        await JSRuntime.InvokeVoidAsync("open", url, "_blank");
    }

    private async Task<bool> IsLastFmTaskNeeded()
    {
        var sessionKey = await StoreGetter.GetValue<string>(StoreKey.LastFmSessionKey);
        return sessionKey == null;
    }

    private async Task<string> GetAuthUrl()
    {
        return await LastFmAuthoriser.GetAuthUrl(RedirectUri);
    }

    private async Task<string> Authorise(string authUrl)
    {
        await NavigateToNewTab(authUrl);

        var startTime = DateTime.Now;
        var endTime = startTime.AddSeconds(60);

        var token = await StoreGetter.GetValue<string>(StoreKey.LastFmToken);

        while (token == null && DateTime.Now < endTime)
        {
            await Task.Delay(500);
            token = await StoreGetter.GetValue<string>(StoreKey.LastFmToken);
        }

        if (token == null)
            throw new Exception("No token received - need to authenticate on Last.FM website");

        return token.Value;
    }

    private async Task<string> CreateSession(string token)
    {
        // Need to handle specific error e.g. if session key has been revoked
        return await LastFmAuthoriser.CreateSession(token);
    }

    private async Task SaveSessionKey(string sessionKey)
    {
        await StoreSetter.SetValue(StoreKey.LastFmSessionKey, sessionKey);
        await StoreSetter.Clear(StoreKey.LastFmToken);
    }
}