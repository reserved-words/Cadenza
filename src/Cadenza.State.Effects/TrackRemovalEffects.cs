namespace Cadenza.State.Effects;

public class TrackRemovalEffects
{
    private readonly IUpdateRepository _repository;

    public TrackRemovalEffects(IUpdateRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleTrackRemovedAction(TrackRemovalRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.RemoveTrack(action.TrackId);
            dispatcher.Dispatch(new TrackRemovedAction(action.TrackId, null));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new TrackRemovedAction(action.TrackId, ex.Message));
        }
    }

    [EffectMethod(typeof(TrackRemovedAction))]
    public Task HandleTrackRemovedAction(IDispatcher dispatcher)
    {
        // Reload all search items - overkill really but leave for now
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        return Task.CompletedTask;
    }
}
