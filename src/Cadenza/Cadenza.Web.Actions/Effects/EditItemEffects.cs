namespace Cadenza.Web.Actions.Effects;

public class EditItemEffects
{
    private readonly ILibraryApi _api;

    public EditItemEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public Task HandleCancelEditItemRequest(CancelEditItemRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleRemoveEditItemRequest(RemoveEditItemRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationErrorRequest("Remove item not implemented yet", null, null));
        return Task.CompletedTask;

    }

    [EffectMethod]
    public Task HandleSaveEditItemRequest(SaveEditItemRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationErrorRequest("Save item not implemented yet", null, null));
        return Task.CompletedTask;
    }
}
