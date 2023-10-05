namespace Cadenza.State.Actions.Effects;

public class UpdateEffects
{
    private readonly INotificationService _notificationService;
    private readonly IState<ViewState> _viewState;

    public UpdateEffects(INotificationService notificationService, IState<ViewState> viewState)
    {
        _notificationService = notificationService;
        _viewState = viewState;
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
        return Task.CompletedTask;
    }
}
