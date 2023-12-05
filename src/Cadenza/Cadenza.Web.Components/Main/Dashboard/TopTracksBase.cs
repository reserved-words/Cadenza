namespace Cadenza.Web.Components.Main.Dashboard;

public class TopTracksBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryTracksState> PlayHistoryTracksState { get; set; }

    private const int MaxItems = 5;

    protected IReadOnlyCollection<TopTrackVM> Items => GetItems();
    protected bool IsLoading => PlayHistoryTracksState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryTracksState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryTracksRequest(period));
    }

    private IReadOnlyCollection<TopTrackVM> GetItems()
    {
        var items = new List<TopTrackVM>(PlayHistoryTracksState.Value.Items.Take(MaxItems));

        while (items.Count() < MaxItems)
        {
            items.Add(new TopTrackVM(0, "-", "-", 0, items.Count + 1));
        }

        return items;
    }
}
