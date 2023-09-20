using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.State.Effects;

public class TrackEndedEffects
{
    [EffectMethod]
    public Task HandleTrackEndedAction(TrackEndedAction action, IDispatcher dispatcher)
    {
        if (action.IsLastTrackInPlaylist)
        {
            dispatcher.Dispatch(new PlaylistStopRequest());
        }
        else
        {
            dispatcher.Dispatch(new PlaylistMoveNextRequest());
        }
        return Task.CompletedTask;
    }
}
