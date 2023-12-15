namespace Cadenza.Web.Components.Main.Dashboard;

public class RecentlyAddedAlbumsBase : FluxorComponent
{
    [Inject] public IState<RecentlyAddedAlbumsState> AlbumHistoryState { get; set; }

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => AlbumHistoryState.Value.Albums.Take(8).ToList();

    protected bool IsLoading => AlbumHistoryState.Value.IsLoading;
}
