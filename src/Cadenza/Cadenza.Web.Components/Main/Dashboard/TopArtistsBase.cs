namespace Cadenza.Web.Components.Main.Dashboard;

public class TopArtistsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryArtistsState> PlayHistoryArtistsState { get; set; }

    protected IReadOnlyCollection<TopArtistVM> Items => PlayHistoryArtistsState.Value.Items.Take(10).ToList();

    protected bool IsLoading => PlayHistoryArtistsState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryArtistsState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryArtistsRequest(period));
    }
}
