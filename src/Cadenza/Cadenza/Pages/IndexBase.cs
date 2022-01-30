namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IStoreSetter Store { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    public bool IsSpotifyLoaded { get; private set; }
    public bool IsLastFmLoaded { get; private set; } = true; // todo
    public bool IsLocalLoaded { get; private set; } = true; // todo

    public bool IsAllLoaded => IsSpotifyLoaded && IsLastFmLoaded && IsLocalLoaded;

    protected override async Task OnInitializedAsync()
    {
        App.SourceEnabled += OnSourceEnabled;
        App.SourceErrored += OnSourceErrored;

        await Store.SetValue(StoreKey.CurrentTrackSource, null);
        await Store.SetValue(StoreKey.CurrentTrack, null);
    }

    private Task OnSourceErrored(object sender, SourceEventArgs e)
    {
        if (e.Source == LibrarySource.Spotify)
        {
            IsSpotifyLoaded = true;
        }

        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnSourceEnabled(object sender, SourceEventArgs e)
    {
        if (e.Source == LibrarySource.Spotify)
        {
            IsSpotifyLoaded = true;
        }

        StateHasChanged();
        return Task.CompletedTask;
    }
}