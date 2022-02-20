namespace Cadenza;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IStoreGetter Store { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    public TrackFull Model { get; set; }

    public bool NotCurrentlyPlaying => Model == null;

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        Model = (await Store.GetValue<TrackFull>(StoreKey.CurrentTrack)).Value;
        StateHasChanged();
    }

    private async Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        Model = null;
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