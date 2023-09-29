namespace Cadenza.State.Effects;

public class TrackUpdateEffects
{
    private readonly IUpdateRepository _repository;

    public TrackUpdateEffects(IUpdateRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleTrackUpdateRequest(TrackUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateTrack(action.Update);
            dispatcher.Dispatch(new TrackUpdatedAction(action.TrackId, action.Update));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Track, action.TrackId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new TrackUpdateFailedAction(action.TrackId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Track, action.TrackId, ex.Message, ex.StackTrace));
        }
    }
}
