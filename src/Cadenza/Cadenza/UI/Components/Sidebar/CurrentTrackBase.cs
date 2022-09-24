using Cadenza.Core.CurrentlyPlaying;
using Cadenza.Core.Player;
using Cadenza.Domain.Extensions;
using Cadenza.Domain.Models.Track;

namespace Cadenza.UI.Components.Sidebar;

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

    public string AlbumId => _model?.Album.Id;
    public string AlbumArtistId => _model?.Album.ArtistId;
    public string TrackId => _model?.Track.Id;
    public string TrackArtistId => _model?.Artist.Id;

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
}
