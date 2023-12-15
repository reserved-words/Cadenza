namespace Cadenza.Web.Components.Main.Dashboard;

public class RecentlyPlayedAlbumsBase : FluxorComponent
{
    [Inject] public IState<PlaylistHistoryAlbumsState> AlbumHistoryState { get; set; }

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => AlbumHistoryState.Value.Items.Take(12).ToList();

    protected bool IsLoading => AlbumHistoryState.Value.IsLoading;
}
