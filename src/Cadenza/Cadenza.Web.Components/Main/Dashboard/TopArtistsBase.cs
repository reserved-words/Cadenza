namespace Cadenza.Web.Components.Main.Dashboard;

public class TopArtistsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryArtistsState> PlayHistoryArtistsState { get; set; }

    private const int MaxItems = 8;

    protected IReadOnlyCollection<TopArtistVM> Items => GetItems();

    protected bool IsLoading => PlayHistoryArtistsState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryArtistsState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryArtistsRequest(period));
    }

    private IReadOnlyCollection<TopArtistVM> GetItems()
    {
        var items = new List<TopArtistVM>(PlayHistoryArtistsState.Value.Items.Take(MaxItems));

        while (items.Count() < MaxItems)
        {
            items.Add(new TopArtistVM(0, "-", 0, items.Count + 1));
        }

        return items;
    }
}
