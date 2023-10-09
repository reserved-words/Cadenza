namespace Cadenza.Web.Actions.Effects;

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
            await _repository.UpdateTrack(action.OriginalTrack, action.UpdatedTrack);
            dispatcher.Dispatch(new TrackUpdatedAction(action.UpdatedTrack));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Track, action.OriginalTrack.Id));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new TrackUpdateFailedAction(action.OriginalTrack.Id));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Track, action.OriginalTrack.Id, ex.Message, ex.StackTrace));
        }
    }
}
