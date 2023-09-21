﻿namespace Cadenza.State.Effects;

public class TrackEndedEffects
{
    [EffectMethod]
    public Task HandleTrackEndedAction_PlaylistUpdate(TrackEndedAction action, IDispatcher dispatcher)
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

    [EffectMethod(typeof(TrackEndedAction))]
    public Task HandleTrackEndedAction_ProgressUpdate(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayProgressResetAction(0));
        return Task.CompletedTask;  
    }
}
