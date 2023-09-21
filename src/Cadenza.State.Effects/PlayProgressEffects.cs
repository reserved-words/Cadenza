namespace Cadenza.State.Effects;

public class PlayProgressEffects
{
    private readonly IState<CurrentTrackState> _currentTrackState;
    private readonly IState<PlayProgressState> _playProgressState;

    public PlayProgressEffects(IState<PlayProgressState> state, IState<CurrentTrackState> currentTrackState)
    {
        _playProgressState = state;
        _currentTrackState = currentTrackState;
    }

    [EffectMethod]
    public Task HandlePlayProgressIncrementAction(PlayProgressIncrementAction action, IDispatcher dispatcher)
    {
        if (_playProgressState.Value.SecondsPlayed >= _playProgressState.Value.TotalSeconds)
        {
            dispatcher.Dispatch(new TrackEndedAction(_currentTrackState.Value.IsLastInPlaylist));
        }
        return Task.CompletedTask;
    }
}
