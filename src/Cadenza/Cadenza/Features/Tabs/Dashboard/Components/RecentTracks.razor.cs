namespace Cadenza.Features.Tabs.Dashboard.Components;

public class RecentTracksBase : FluxorComponent
{
    [Inject] public IState<HistoryRecentlyPlayedTracksState> State { get; set; }

    public IReadOnlyCollection<RecentTrackVM> Model => State.Value.Tracks;

    public bool IsLoading => State.Value.Tracks == null;
}