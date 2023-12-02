using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Components.Main.History;

public class RecentAlbumsBase : FluxorComponent
{
    [Inject] public IState<PlaylistHistoryAlbumsState> AlbumHistoryState { get; set; }

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => AlbumHistoryState.Value.Items.Take(12).ToList();

    protected bool IsLoading => AlbumHistoryState.Value.IsLoading;
}
