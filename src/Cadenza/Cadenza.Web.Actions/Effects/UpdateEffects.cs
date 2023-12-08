namespace Cadenza.Web.Actions.Effects;

public class UpdateEffects
{
    [EffectMethod]
    public Task HandleUpdateFailedAction(UpdateFailedAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationErrorRequest("Update failed", action.Error, action.StackTrace));
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleUpdateSucceededAction(UpdateSucceededAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationSuccessRequest("Update succeeded"));
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;
    }
}
