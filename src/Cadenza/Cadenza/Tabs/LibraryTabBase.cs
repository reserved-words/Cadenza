using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Tabs;

public class LibraryTabBase : FluxorComponent
{
    [Inject] public IMessenger Messenger { get; set; }
    [Inject] public IState<ViewState> ViewState { get; set; }

    protected ViewItem? CurrentItem => ViewState.Value.Item;
}
