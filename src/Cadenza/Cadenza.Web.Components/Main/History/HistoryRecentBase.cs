using Fluxor;

namespace Cadenza.Web.Components.Main.History;

public class HistoryRecentBase : FluxorComponent
{
    [Inject] public IState<RecentPlayHistoryState> RecentPlayHistoryState { get; set; }

    public List<RecentTrack> Model => RecentPlayHistoryState.Value.Tracks;

    public bool IsLoading => RecentPlayHistoryState.Value.Tracks == null;
}