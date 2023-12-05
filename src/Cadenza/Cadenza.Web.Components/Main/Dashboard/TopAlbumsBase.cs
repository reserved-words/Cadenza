using Cadenza.Web.Common;

namespace Cadenza.Web.Components.Main.Dashboard;

public class TopAlbumsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryAlbumsState> PlayHistoryAlbumsState { get; set; }

    private const int MaxItems = 5;

    protected IReadOnlyCollection<TopAlbumVM> Items => GetItems();
    protected bool IsLoading => PlayHistoryAlbumsState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryAlbumsState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryAlbumsRequest(period));
    }

    private IReadOnlyCollection<TopAlbumVM> GetItems()
    {
        var items = new List<TopAlbumVM>(PlayHistoryAlbumsState.Value.Items.Take(MaxItems));

        while (items.Count() < MaxItems)
        {
            items.Add(new TopAlbumVM(0, "-", "-", ImageUrl.Default, 0, items.Count + 1));
        }

        return items;
    }
}
