using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.State.Effects;

public class PlayStatusEffects
{
    [EffectMethod(typeof(PlayStatusPlayingAction))]
    public static Task HandlePlayStatusPlayingAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new FetchRecentPlayHistoryAction());
        return Task.CompletedTask;
    }
}
