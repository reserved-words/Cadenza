using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.State.Effects;

public class PlaylistTrackEffects
{
    [EffectMethod]
    public Task HandlePlaylistTrackUpdateAction_FetchCurrentTrack(PlaylistTrackUpdateAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new UpdateCurrentTrackRequest(action.Track.Id));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandlePlaylistTrackUpdateAction_PlayNewTrack(PlaylistTrackUpdateAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerPlayRequest(action.Track));
        return Task.CompletedTask;
    }
}
