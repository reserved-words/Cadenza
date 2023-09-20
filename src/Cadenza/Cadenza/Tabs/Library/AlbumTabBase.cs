using Fluxor;

namespace Cadenza.Tabs.Library;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IMessenger Messenger { get; set; }
    [Inject] public IAlbumRepository Repository { get; set; }
    [Inject] public IState<TrackRemovalState> TrackRemovalState { get; set; }

    [Parameter] public int Id { get; set; }

    public AlbumInfo Album { get; set; }

    public List<Disc> Discs { get; set; } = new();

    protected override void OnInitialized()
    {
        TrackRemovalState.StateChanged += TrackRemovalState_StateChanged;

        base.OnInitialized();
    }

    protected override async Task OnParametersSetAsync()
    {
        await UpdateAlbum();
    }

    private void TrackRemovalState_StateChanged(object sender, EventArgs e)
    {
        var disc = Discs.SingleOrDefault(d => d.Tracks.Any(t => t.TrackId == TrackRemovalState.Value.LastTrackRemovedId));
        if (disc == null)
            return;

        var track = disc.Tracks.Single(t => t.TrackId == TrackRemovalState.Value.LastTrackRemovedId);
        disc.Tracks.Remove(track);
        StateHasChanged();
    }

    private async Task UpdateAlbum()
    {
        Album = await Repository.GetAlbum(Id);

        var tracks = await Repository.GetAlbumTracks(Id);

        Discs = tracks.GroupByDisc();

        StateHasChanged();
    }
}
