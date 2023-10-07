namespace Cadenza.Web.Components.Tabs.Library;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IState<ViewAlbumState> ViewAlbumState { get; set; }

    public bool Loading => ViewAlbumState.Value.IsLoading;
    public AlbumDetailsVM Album => ViewAlbumState.Value.Album;
    public List<DiscVM> Discs => ViewAlbumState.Value.Discs;

    protected override void OnInitialized()
    {
        SubscribeToAction<TrackRemovedAction>(OnTrackRemoved);
        base.OnInitialized();
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
}
