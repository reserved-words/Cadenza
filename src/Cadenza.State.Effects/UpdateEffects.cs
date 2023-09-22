namespace Cadenza.State.Effects;

public class UpdateEffects
{
    private INotificationService _notificationService;

    public UpdateEffects(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [EffectMethod]
    public Task HandleUpdateFailedAction_Notification(UpdateFailedAction action, IDispatcher dispatcher)
    {
        _notificationService.Error($"Update failed: {action.Error}");
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleUpdateFailedAction_Log(UpdateFailedAction action, IDispatcher dispatcher)
    {
        Console.WriteLine(action.Error);
        Console.WriteLine(action.StackTrace);
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleUpdateSucceededAction(UpdateSucceededAction action, IDispatcher dispatcher)
    {
        _notificationService.Success("Update succeeded");
        dispatcher.Dispatch(new SearchItemsUpdateRequest());

        // Depending on item type do other updates?
        
        return Task.CompletedTask;
    }
}
