using Cadenza.Common.Interfaces.Repositories;
using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.State.Effects;

public class CurrentTrackEffects
{
    private readonly ITrackRepository _repository;

    public CurrentTrackEffects(ITrackRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleUpdateCurrentTrackRequest(FetchFullTrackRequest action, IDispatcher dispatcher)
    {
        var fullTrack = await _repository.GetTrack(action.PlayTrack.Id);
        dispatcher.Dispatch(new UpdateCurrentTrackAction(action.PlayTrack, fullTrack, action.IsLastTrackInPlaylist));
    }

    [EffectMethod]
    public Task HandleUpdateCurrentTrackAction_PlayNewTrack(UpdateCurrentTrackAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new PlayerPlayRequest(action.FullTrack.Track));
        return Task.CompletedTask;
    }
}
