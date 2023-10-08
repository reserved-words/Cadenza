namespace Cadenza.Web.Actions.Effects;

public class NotificationEffects
{
    private readonly INotificationService _notificationService;

    public NotificationEffects(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [EffectMethod]
    public Task HandleNotificationErrorRequest(NotificationErrorRequest action, IDispatcher dispatcher)
    {
        if (action.Error == null)
        {
            _notificationService.Error(action.Message);
        }
        else
        {
            _notificationService.Error($"{action.Message}: {action.Error}");
            Console.WriteLine(action.Error);
        }

        if (action.StackTrace != null)
        {
            Console.WriteLine(action.StackTrace);
        }

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleNotificationSuccessRequest(NotificationSuccessRequest action, IDispatcher dispatcher)
    {
        _notificationService.Success(action.Message);
        return Task.CompletedTask;
    }
}
