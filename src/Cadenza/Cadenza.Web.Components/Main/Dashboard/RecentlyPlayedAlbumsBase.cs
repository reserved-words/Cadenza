namespace Cadenza.Web.Components.Main.Dashboard;

public class RecentlyPlayedAlbumsBase : FluxorComponent
{
    [Inject] public IState<HistoryRecentlyPlayedAlbumsState> State { get; set; }

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => State.Value.Items;

    protected bool IsLoading => State.Value.IsLoading;
}
