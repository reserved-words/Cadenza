namespace Cadenza.Features.Tabs.Library;

public class LibraryTabBase : FluxorComponent
{
    [Inject] public IState<ViewState> ViewState { get; set; }

    protected ViewItem? CurrentItem => ViewState.Value.ViewItem;
}
