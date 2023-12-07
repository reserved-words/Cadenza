namespace Cadenza.Web.Components.Tabs;

public class EditTabBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<ViewState> ViewState { get; set; }

    protected EditItem? CurrentItem => ViewState.Value.EditItem;

    protected string CancelCaption => "Cancel";
    protected string RemoveCaption => $"Remove {CurrentItem.Value.Type}";
    protected string SaveCaption => "Save Changes";

    protected void Cancel()
    {
        Dispatcher.Dispatch(new ViewEditEndRequest());
    }
    protected void Remove()
    {
        Dispatcher.Dispatch(new NotificationErrorRequest("Not implemented yet", null, null));
    }
    protected void Save()
    {
        Dispatcher.Dispatch(new NotificationErrorRequest("Not implemented yet", null, null));
    }
}
