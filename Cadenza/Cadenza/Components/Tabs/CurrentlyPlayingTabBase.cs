namespace Cadenza;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IViewModelLibrary Library { get; set; }

    public TrackFull Track { get; set; }

    public bool NotCurrentlyPlaying => Track == null;

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;

        Library.AlbumUpdated += OnAlbumUpdated;
        Library.ArtistUpdated += OnArtistUpdated;
        Library.TrackUpdated += OnTrackUpdated;
    }

    private async Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs e)
    {
        if (Track == null || Track.Album.Id != e.Update.Id)
            return;

        await SetTrack(Track.Track.Source, Track.Track.Id);
    }

    private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        if (Track == null || Track.Artist.Id != e.Update.Id)
            return;

        await SetTrack(Track.Track.Source, Track.Track.Id);
    }

    private async Task OnTrackUpdated(object sender, TrackUpdatedEventArgs e)
    {
        if (Track == null || Track.Track.Id != e.Update.Id)
            return;

        await SetTrack(Track.Track.Source, Track.Track.Id);
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        await SetTrack(e.CurrentTrack.Source, e.CurrentTrack.Id);
    }

    private async Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        Track = null;
        StateHasChanged();
    }

    private async Task SetTrack(LibrarySource source, string trackId)
    {
        Track = await Library.GetTrack(source, trackId);
        StateHasChanged();
    }
}