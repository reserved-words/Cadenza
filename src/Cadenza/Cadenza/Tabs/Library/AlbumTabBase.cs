namespace Cadenza.Tabs.Library;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IMessenger Messenger { get; set; }
    [Inject] public IAlbumRepository Repository { get; set; }

    [Parameter] public int Id { get; set; }

    public AlbumInfo Album { get; set; }

    public List<Disc> Discs { get; set; } = new();

    protected override void OnInitialized()
    {
        SubscribeToAction<TrackRemovedAction>(OnTrackRemoved);
        base.OnInitialized();
    }

    protected override async Task OnParametersSetAsync()
    {
        await UpdateAlbum();
    }

    private void OnTrackRemoved(TrackRemovedAction action)
    {
        var disc = Discs.SingleOrDefault(d => d.Tracks.Any(t => t.TrackId == action.TrackId));
        if (disc == null)
            return;

        var track = disc.Tracks.Single(t => t.TrackId == action.TrackId);
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
