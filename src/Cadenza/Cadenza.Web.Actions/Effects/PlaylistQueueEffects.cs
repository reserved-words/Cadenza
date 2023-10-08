namespace Cadenza.Web.Actions.Effects;

public class PlaylistQueueEffects
{
    private IState<PlaylistQueueState> _state;

    public PlaylistQueueEffects(IState<PlaylistQueueState> state)
    {
        _state = state;
    }

    [EffectMethod(typeof(PlaylistQueueUpdateRequest))]
    public Task HandlePlaylistQueueUpdateRequest(IDispatcher dispatcher)
    {
        return DispatchFetchTrackRequest(dispatcher);
    }

    [EffectMethod(typeof(PlaylistQueueMoveNextRequest))]
    public Task HandlePlaylistQueueMoveNextRequest(IDispatcher dispatcher)
    {
        return DispatchFetchTrackRequest(dispatcher);
    }

    [EffectMethod(typeof(PlaylistQueueMovePreviousRequest))]
    public Task HandlePlaylistQueueMovePreviousRequest(IDispatcher dispatcher)
    {
        return DispatchFetchTrackRequest(dispatcher);
    }

    private Task DispatchFetchTrackRequest(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new FetchTrackRequest(_state.Value.CurrentTrack, _state.Value.IsCurrentTrackLast));
        return Task.CompletedTask;
    }
}
