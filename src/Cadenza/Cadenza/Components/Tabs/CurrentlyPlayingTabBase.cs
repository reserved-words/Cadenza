using Cadenza.Library;

namespace Cadenza;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IMergedTrackRepository Repository { get; set; }

    [Inject]
    public IPlaylistPlayer Player { get; set; }

    public TrackFull Model { get; set; }

    public bool NotCurrentlyPlaying => Model == null;

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;
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
        Model = source.HasValue
            ? await Repository.GetTrack(source.Value, trackId)
            : null;

        StateHasChanged();
    }

    protected async Task OnPlayTrack(Track track)
    {
        await Player.PlayTrack(track.Source, track.Id);
    }

    protected async Task OnPlayAlbum(Album album)
    {
        await Player.PlayAlbum(album.Source, album.Id);
    }

    protected async Task OnPlayArtist(Artist artist)
    {
        await Player.PlayArtist(artist.Id);
    }
}