namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IStoreSetter Store { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    public bool IsSpotifyLoaded => App.IsInitialised(Connector.Spotify);
    public bool IsLastFmLoaded => App.IsInitialised(Connector.LastFm);
    public bool IsLocalLoaded => App.IsInitialised(Connector.Local);

    public bool IsAllLoaded => IsSpotifyLoaded && IsLastFmLoaded && IsLocalLoaded;

    protected override async Task OnInitializedAsync()
    {
        App.ConnectorEnabled += OnConnectorInitialised;
        App.ConnectorDisabled += OnConnectorInitialised;

        await Store.SetValue(StoreKey.CurrentTrackSource, null);
        await Store.SetValue(StoreKey.CurrentTrack, null);
    }

    private Task OnConnectorInitialised(object sender, ConnectorEventArgs e)
    {
        StateHasChanged();
        return Task.CompletedTask;
    }
}