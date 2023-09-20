using Cadenza.State.Actions;
using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.State.Effects;

public class PlaylistEffects
{
    private readonly IState<CurrentTrackState> _currentTrackState;

    public PlaylistEffects(IState<CurrentTrackState> currentTrackState)
    {
        _currentTrackState = currentTrackState;
    }

    [EffectMethod]
    public Task HandlePlaylistStartRequest(PlaylistStartRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerStopRequest(_currentTrackState.Value.Track));
        dispatcher.Dispatch(new PlaylistQueueUpdateRequest(action.Definition));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandlePlaylistMoveNextRequest(PlaylistMoveNextRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerStopRequest(_currentTrackState.Value.Track));
        dispatcher.Dispatch(new PlaylistQueueMoveNextRequest());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandlePlaylistMovePreviousRequest(PlaylistMovePreviousRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerStopRequest(_currentTrackState.Value.Track));
        dispatcher.Dispatch(new PlaylistQueueMovePreviousRequest());
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(PlaylistStopRequest))]
    public Task HandlePlaylistStopRequest(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerStopRequest(_currentTrackState.Value.Track));
        dispatcher.Dispatch(new PlaylistQueueUpdateRequest(null));
        return Task.CompletedTask;
    }
}
