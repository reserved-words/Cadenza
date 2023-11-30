namespace Cadenza.Web.Components.Main.History;

public class RecentAlbumsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlaylistHistoryAlbumsState> AlbumHistoryState { get; set; }

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => AlbumHistoryState.Value.Items.Take(12).ToList();

    protected bool IsLoading => AlbumHistoryState.Value.IsLoading;
}
