using Cadenza.Common.Enums;
using Cadenza.Web.Model;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Components.Main.History;

public class HistoryAlbumsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryAlbumsState> PlayHistoryAlbumsState { get; set; }

    protected IReadOnlyCollection<PlayedAlbumVM> Items => PlayHistoryAlbumsState.Value.Items;
    protected bool IsLoading => PlayHistoryAlbumsState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryAlbumsState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryAlbumsRequest(period));
    }
}
