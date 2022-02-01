namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IStoreSetter Store { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    public bool IsSpotifyLoaded => false; // App.IsInitialised(Connector.Spotify);
    public bool IsLastFmLoaded => false;// App.IsInitialised(Connector.LastFm);
    public bool IsLocalLoaded => false; //App.IsInitialised(Connector.Local);

    public bool IsAllLoaded => IsSpotifyLoaded && IsLastFmLoaded && IsLocalLoaded;

    protected override async Task OnInitializedAsync()
    {
        //App.ConnectorEnabled += OnConnectorInitialised;
        //App.ConnectorDisabled += OnConnectorInitialised;

        await Store.SetValue(StoreKey.CurrentTrackSource, null);
        await Store.SetValue(StoreKey.CurrentTrack, null);
        await Store.SetValue(StoreKey.LastFmSessionKey, null);
        await Store.SetValue(StoreKey.SpotifyAccessToken, null);
        await Store.SetValue(StoreKey.SpotifyRefreshToken, null);
        await Store.SetValue(StoreKey.SpotifyDeviceId, null);
    }

    private Task OnConnectorInitialised(object sender, ConnectorEventArgs e)
    {
        StateHasChanged();
        return Task.CompletedTask;
    }
}