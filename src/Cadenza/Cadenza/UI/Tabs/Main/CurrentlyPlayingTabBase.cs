using Cadenza.Core.App;
using Cadenza.Core.CurrentlyPlaying;

namespace Cadenza.UI.Tabs.Main;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IStoreGetter Store { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

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

    private Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        Model = null;
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected async Task OnPlayTrack(Track track)
    {
        await Player.PlayTrack(track.Id);
    }

    protected async Task OnPlayAlbum(Album album)
    {
        await Player.PlayAlbum(album.Id);
    }

    protected async Task OnPlayArtist(Artist artist)
    {
        await Player.PlayArtist(artist.Id);
    }

    protected async Task OnViewTrack()
    {
        await Viewer.ViewTrack(Model.Track);
    }

    protected async Task OnViewAlbum()
    {
        await Viewer.ViewAlbum(Model.Album);
    }

    protected async Task OnViewArtist()
    {
        await Viewer.ViewArtist(Model.Artist);
    }
}