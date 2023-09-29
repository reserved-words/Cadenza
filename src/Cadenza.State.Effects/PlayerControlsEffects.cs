namespace Cadenza.State.Effects;

public class PlayerControlsEffects
{
    private readonly IState<CurrentTrackState> _currentTrackState;

    public PlayerControlsEffects(IState<CurrentTrackState> currentTrackState)
    {
        _currentTrackState = currentTrackState;
    }


    [EffectMethod]
    public Task HandlePlayerPauseRequest(PlayerControlsPauseRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerPauseRequest(_currentTrackState.Value.Track));
        return Task.CompletedTask;
    }


    [EffectMethod]
    public Task PlayerControlsResumeRequest(PlayerControlsResumeRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerResumeRequest(_currentTrackState.Value.Track));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandlePlayerControlsNextRequest(PlayerControlsNextRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerStopRequest(_currentTrackState.Value.Track));

        if (_currentTrackState.Value.IsLastInPlaylist)
        {
            dispatcher.Dispatch(new PlaylistStopRequest());
        }
        else
        {
            dispatcher.Dispatch(new PlaylistQueueMoveNextRequest());
        }

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandlePlayerControlsPreviousRequest(PlayerControlsPreviousRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerStopRequest(_currentTrackState.Value.Track));
        dispatcher.Dispatch(new PlaylistQueueMovePreviousRequest());
        return Task.CompletedTask;
    }
}
