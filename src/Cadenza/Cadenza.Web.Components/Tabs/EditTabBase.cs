namespace Cadenza.Web.Components.Tabs;

public class EditTabBase : ComponentBase
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    protected void ResetTabs()
    {
        Dispatcher.Dispatch(new ViewResetRequest());
    }
}
