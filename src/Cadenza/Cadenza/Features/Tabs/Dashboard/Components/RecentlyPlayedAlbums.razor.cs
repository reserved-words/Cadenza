namespace Cadenza.Features.Tabs.Dashboard.Components;

public class RecentlyPlayedAlbumsBase : FluxorComponent
{
    [Inject] public IState<HistoryRecentlyPlayedAlbumsState> State { get; set; }

    [Parameter] public bool ShowHeader { get; set; } = true;

    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => State.Value.Items;

    protected bool IsLoading => State.Value.IsLoading;
}
