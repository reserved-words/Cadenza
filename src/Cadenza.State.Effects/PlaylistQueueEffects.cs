using Cadenza.State.Actions;
using Cadenza.State.Store;
using Fluxor;
using System;

namespace Cadenza.State.Effects;

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
        return DispatchUpdatePlaylistTrackAction(dispatcher);
    }

    [EffectMethod(typeof(PlaylistQueueMoveNextRequest))]
    public Task HandlePlaylistQueueMoveNextRequest(IDispatcher dispatcher)
    {
        return DispatchUpdatePlaylistTrackAction(dispatcher);
    }

    [EffectMethod(typeof(PlaylistQueueMovePreviousRequest))]
    public Task HandlePlaylistQueueMovePreviousRequest(IDispatcher dispatcher)
    {
        return DispatchUpdatePlaylistTrackAction(dispatcher);
    }

    private Task DispatchUpdatePlaylistTrackAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new FetchFullTrackRequest(_state.Value.CurrentTrack, _state.Value.IsCurrentTrackLast));
        return Task.CompletedTask;
    }
}
