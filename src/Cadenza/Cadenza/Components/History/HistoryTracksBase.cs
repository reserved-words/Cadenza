using Fluxor;

namespace Cadenza.Components.History;

public class HistoryTracksBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryTracksState> PlayHistoryTracksState { get; set; }

    protected List<PlayedTrack> Items => PlayHistoryTracksState.Value.Items;
    protected bool IsLoading => PlayHistoryTracksState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryTracksState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryTracksRequest(period));
    }
}
