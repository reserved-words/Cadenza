using Cadenza.Web.State.Actions;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Actions.Effects;

public class PlayProgressEffects
{
    private readonly IState<PlayProgressState> _playProgressState;

    public PlayProgressEffects(IState<PlayProgressState> state)
    {
        _playProgressState = state;
    }

    [EffectMethod]
    public Task HandlePlayProgressIncrementAction(PlayProgressIncrementAction action, IDispatcher dispatcher)
    {
        if (_playProgressState.Value.SecondsPlayed >= _playProgressState.Value.TotalSeconds)
        {
            dispatcher.Dispatch(new TrackEndedAction());
        }
        return Task.CompletedTask;
    }
}
