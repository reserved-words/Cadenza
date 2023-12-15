using Cadenza.Web.Common;

namespace Cadenza.Web.Components.Main.Dashboard;

public class TopAlbumsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<HistoryTopPlayedAlbumsState> State { get; set; }

    protected IReadOnlyCollection<TopAlbumVM> Items => GetItems();
    protected bool IsLoading => State.Value.IsLoading;
    protected HistoryPeriod Period => State.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchTopPlayedAlbumsRequest(period));
    }

    private IReadOnlyCollection<TopAlbumVM> GetItems()
    {
        var items = new List<TopAlbumVM>(State.Value.Items);

        while (items.Count < Constants.MaxTopPlayedAlbums)
        {
            items.Add(new TopAlbumVM(0, "-", "-", ImageUrl.Default, 0, items.Count + 1));
        }

        return items;
    }
}
