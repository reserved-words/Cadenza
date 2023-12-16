namespace Cadenza.Web.Components.Tabs;

public class SettingsTabBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    protected void Save()
    {
        Dispatcher.Dispatch(new NotificationInformationRequest("Save implemented yet"));
    }

    protected void Cancel()
    {
        Dispatcher.Dispatch(new ViewResetRequest());
    }
}