namespace Cadenza.Web.Components.Main.Dashboard;

public class RecentTracksBase : FluxorComponent
{
    [Inject] public IState<RecentPlayHistoryState> RecentPlayHistoryState { get; set; }

    public IReadOnlyCollection<RecentTrackVM> Model => RecentPlayHistoryState.Value.Tracks;

    public bool IsLoading => RecentPlayHistoryState.Value.Tracks == null;
}