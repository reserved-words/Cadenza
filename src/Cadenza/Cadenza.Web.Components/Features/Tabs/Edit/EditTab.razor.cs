namespace Cadenza.Web.Components.Features.Tabs.Edit;

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
        Dispatcher.Dispatch(new CancelEditItemRequest());
    }
    protected void Remove()
    {
        Dispatcher.Dispatch(new RemoveEditItemRequest(CurrentItem.Value.Type, CurrentItem.Value.Id));
    }
    protected void Save()
    {
        Dispatcher.Dispatch(new SaveEditItemRequest(CurrentItem.Value.Type, CurrentItem.Value.Id));
    }
}
