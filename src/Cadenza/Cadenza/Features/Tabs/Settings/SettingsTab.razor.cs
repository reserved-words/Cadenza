namespace Cadenza.Features.Tabs.Settings;

public partial class SettingsTab
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