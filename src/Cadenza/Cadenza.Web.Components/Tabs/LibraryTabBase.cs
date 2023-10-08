namespace Cadenza.Web.Components.Tabs;

public class LibraryTabBase : FluxorComponent
{
    [Inject] public IState<ViewState> ViewState { get; set; }

    protected ViewItem? CurrentItem => ViewState.Value.Item;
}
