namespace Cadenza.Features.Tabs.Library;

public partial class LibraryTab
{
    [Inject] public IState<ViewState> ViewState { get; set; }

    protected ViewItem? CurrentItem => ViewState.Value.ViewItem;
}
