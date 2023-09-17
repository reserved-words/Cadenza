using Cadenza.State.Store;
using Cadenza.Web.Common.Interfaces.Connections;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Components.History;

public class HistoryRecentBase : FluxorComponent
{
    // TODO: currently it will check first if the last.FM status is errored or disconnected to avoid problems - how to handle this now?
    // TODO: MaxItems was previously provided as a parameter - how to handle this now?
    // TODO: Also note that FetchRecentPlayHistoryAction needs to be triggered when the app is loaded, not only when the play status changes

    [Inject]
    public IConnectionService ConnectorService { get; set; }

    [Inject]
    public IState<RecentPlayHistoryState> RecentPlayHistoryState { get; set; }

    [Parameter]
    public int MaxItems { get; set; }

    public List<RecentTrack> Model => RecentPlayHistoryState.Value.Tracks;

    public bool IsLoading => RecentPlayHistoryState.Value.Tracks == null;
}