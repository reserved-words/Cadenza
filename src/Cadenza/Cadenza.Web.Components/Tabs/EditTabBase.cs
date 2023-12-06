namespace Cadenza.Web.Components.Tabs;

public class EditTabBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<ViewState> ViewState { get; set; }

    protected EditItem? CurrentItem => ViewState.Value.EditItem;

    protected void ResetTabs()
    {
        Dispatcher.Dispatch(new ViewEditEndRequest());
    }
}
