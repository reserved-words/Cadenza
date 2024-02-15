namespace Cadenza.Features.Tabs.Dashboard.Components;

public class RecentlyAddedAlbumsBase : FluxorComponent
{
    [Inject] public IState<HistoryRecentlyAddedAlbumsState> State { get; set; }

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => State.Value.Albums;

    protected bool IsLoading => State.Value.IsLoading;
}
