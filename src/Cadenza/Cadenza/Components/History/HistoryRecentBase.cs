using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Components.History;

public class HistoryRecentBase : FluxorComponent
{
    [Inject]
    public IState<RecentPlayHistoryState> RecentPlayHistoryState { get; set; }

    public List<RecentTrack> Model => RecentPlayHistoryState.Value.Tracks;

    public bool IsLoading => RecentPlayHistoryState.Value.Tracks == null;
}