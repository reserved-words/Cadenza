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

    [Inject]
    public IItemViewer Viewer { get; set; }

    public bool Loading { get; set; } = false;

    public bool Empty => _model == null && !Loading;

    public double Progress { get; set; }

    public string AlbumArtist => _model?.Album.ArtistName ?? "Album Artist";

    public string AlbumTitle => _model?.Album.Title ?? "Album Title";

    public string Artist => _model?.Artist.Name ?? "Artist Name";

    public string ArtworkUrl => _model?.Album.ArtworkUrl ?? ArtworkPlaceholderUrl;

    public string ReleaseType => _model?.Album.ReleaseType.GetDisplayName() ?? "Release Type";

    public string SourceIcon => _model?.Track.Source.GetIcon();

    public string Title => _model?.Track.Title ?? "Track Title";

    public string Year => _model?.Album.Year ?? "Year";

    private TrackFull _model;

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
        _model = (await Store.GetValue<TrackFull>(StoreKey.CurrentTrack)).Value;
        StateHasChanged();
    }

    private void OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
    }

    protected async Task OnViewAlbum()
    {
        await Viewer.ViewAlbum(_model.Album);
    }

    protected async Task OnViewArtist()
    {
        await Viewer.ViewArtist(_model.Artist);
    }

    protected async Task OnViewTrack()
    {
        await Viewer.ViewTrack(_model.Track);
    }

    protected async Task OnViewAlbumArtist()
    {
        await Viewer.ViewArtist(_model.Album.ArtistId, _model.Album.ArtistName);
    }
}
