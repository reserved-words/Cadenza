﻿using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.State.Effects;

public class PlaylistEffects
{

    [EffectMethod]
    public Task HandlePlaylistStartRequest(PlaylistStartRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlaylistQueueUpdateRequest(action.Definition));
        return Task.CompletedTask;
    }
}
