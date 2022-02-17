using Cadenza.Utilities;

namespace Cadenza;

public class CurrentTrackBase : ComponentBase
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    [Inject]
    public ITrackProgressedConsumer TrackProgressConsumer { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IStoreGetter Store { get; set; }

    public bool Loading { get; set; } = false;

    public bool Empty => _model == null && !Loading;

    public double Progress { get; set; }

    public string AlbumArtist => _model?.AlbumArtist ?? "Album Artist";

    public string AlbumTitle => _model?.AlbumTitle ?? "Album Title";

    public string Artist => _model?.Artist ?? "Artist Name";

    public string ArtworkUrl => _model?.ArtworkUrl ?? ArtworkPlaceholderUrl;

    public string ReleaseType => _model?.ReleaseType.GetDisplayName() ?? "Release Type";

    public string SourceIcon => _model?.Source.GetIcon();

    public string Title => _model?.Title ?? "Track Title";

    public string Year => _model?.Year ?? "Year";

    private TrackSummary _model;

    protected override void OnInitialized()
    {
        App.PlaylistLoading += OnPlaylistLoading;
        App.PlaylistFinished += OnPlaylistFinished;

        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;

        TrackProgressConsumer.TrackProgressed += OnTrackProgressed;
    }

    private Task OnPlaylistFinished(object sender, PlaylistEventArgs e)
    {
        Loading = false;
        _model = null;
        Progress = 0;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnPlaylistLoading(object sender, PlaylistEventArgs e)
    {
        _model = null;
        Loading = true;
        Progress = 0;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        if (e.IsLastTrack)
        {
            _model = null;
            Progress = 0;
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        Loading = false;
        Progress = 0;
        _model = (await Store.GetValue<TrackSummary>(StoreKey.CurrentTrack)).Value;
        StateHasChanged();
    }

    private void OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
    }
}
