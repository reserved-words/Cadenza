using Cadenza.Web.State.Actions;

namespace Cadenza.Web.Actions.Effects;

public class ViewTrackEffects
{
    private readonly ITrackRepository _repository;

    public ViewTrackEffects(ITrackRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchViewTrackRequest(FetchViewTrackRequest action, IDispatcher dispatcher)
    {
        var track = await _repository.GetTrack(action.TrackId);
        dispatcher.Dispatch(new FetchViewTrackResult(track));
    }
}
