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
    public async Task HandleUpdateCurrentTrackRequest(UpdateCurrentTrackRequest action, IDispatcher dispatcher)
    {
        var result = await _repository.GetTrack(action.TrackId);
        dispatcher.Dispatch(new UpdateCurrentTrackAction(result));
    }
}
