using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.State.Effects;

public class PlayStatusEffects
{
    [EffectMethod]
    public static Task HandlePlayStatusPlayingAction(PlayStatusPlayingAction action, IDispatcher dispatcher)
    {
        // Note should only actually do this if the current track has changed - TODO
        // But also this should probably happen sooner anyway, 
        dispatcher.Dispatch(new FetchCurrentTrackAction(action.Track.Id));
        dispatcher.Dispatch(new FetchRecentPlayHistoryAction());
        return Task.CompletedTask;
    }
}
