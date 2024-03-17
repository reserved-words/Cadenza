namespace Cadenza.Features.Tabs.Dashboard.Components;

public class RecentlyAddedAlbumsBase : FluxorComponent
{
    [Inject] public IState<HistoryRecentlyAddedAlbumsState> State { get; set; }

    [Parameter] public bool ShowHeader { get; set; } = true;

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => State.Value.Albums;

    protected bool IsLoading => State.Value.IsLoading;
}
