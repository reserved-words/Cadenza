namespace Cadenza.Web.Actions.Effects;

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
        dispatcher.Dispatch(new FetchRecentlyPlayedAlbumsRequest());
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(PlaylistStopRequest))]
    public Task HandlePlaylistStopRequest(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlaylistQueueUpdateRequest(null));
        return Task.CompletedTask;
    }
}
