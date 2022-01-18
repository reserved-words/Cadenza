namespace Cadenza;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public ITrackRepository TrackRepository { get; set; }

    public FullTrack Track { get; set; }

    public bool NotCurrentlyPlaying => Track == null;

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;

        //Library.AlbumUpdated += OnAlbumUpdated;
        //Library.ArtistUpdated += OnArtistUpdated;
        //Library.TrackUpdated += OnTrackUpdated;
    }

    private async Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs e)
    {
        if (Track == null || Track.AlbumId != e.Update.Id)
            return;

        await SetTrack(Track.Source, Track.Id);
    }

    private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        if (Track == null || Track.ArtistId != e.Update.Id)
            return;

        await SetTrack(Track.Source, Track.Id);
    }

    private async Task OnTrackUpdated(object sender, TrackUpdatedEventArgs e)
    {
        if (Track == null || Track.Id != e.Update.Id)
            return;

        await SetTrack(Track.Source, Track.Id);
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        await SetTrack(e.CurrentTrack.Source, e.CurrentTrack.Id);
    }

    private async Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        await SetTrack(null, null);
    }

    private async Task SetTrack(LibrarySource? source, string trackId)
    {
        Track = source.HasValue
            ? await TrackRepository.GetDetails(source.Value, trackId)
            : null;

        StateHasChanged();
    }
}