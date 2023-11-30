namespace Cadenza.Web.Components.Main.History;

public class HistoryTracksBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryTracksState> PlayHistoryTracksState { get; set; }

    protected IReadOnlyCollection<TopTrackVM> Items => PlayHistoryTracksState.Value.Items.Take(5).ToList();
    protected bool IsLoading => PlayHistoryTracksState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryTracksState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryTracksRequest(period));
    }
}
