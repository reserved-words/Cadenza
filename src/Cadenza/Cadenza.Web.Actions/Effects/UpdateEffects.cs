using Cadenza.Common.Enums.Extensions;

namespace Cadenza.Web.Actions.Effects;

public class UpdateEffects
{
    [EffectMethod]
    public Task HandleUpdateFailedAction(UpdateFailedAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationErrorRequest($"{action.Type.GetDisplayName()} update failed", action.Error, action.StackTrace));
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleUpdateSucceededAction(UpdateSucceededAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationSuccessRequest($"{action.Type.GetDisplayName()} update succeeded"));
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;
    }
}
