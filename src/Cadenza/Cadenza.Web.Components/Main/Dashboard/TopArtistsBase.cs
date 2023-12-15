using Cadenza.Web.Common;

namespace Cadenza.Web.Components.Main.Dashboard;

public class TopArtistsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<HistoryTopPlayedArtistsState> State { get; set; }

    protected IReadOnlyCollection<TopArtistVM> Items => GetItems();

    protected bool IsLoading => State.Value.IsLoading;
    protected HistoryPeriod Period => State.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchTopPlayedArtistsRequest(period));
    }

    private IReadOnlyCollection<TopArtistVM> GetItems()
    {
        var items = new List<TopArtistVM>(State.Value.Items);

        while (items.Count < Constants.MaxTopPlayedArtists)
        {
            items.Add(new TopArtistVM(0, "-", 0, items.Count + 1));
        }

        return items;
    }
}
