namespace Cadenza.Web.Actions.Effects;

public class TrackUpdateEffects
{
    private readonly IUpdateApi _api;

    public TrackUpdateEffects(IUpdateApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleTrackUpdateRequest(TrackUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _api.UpdateTrack(action.UpdatedTrack);
            dispatcher.Dispatch(new TrackUpdatedAction(action.UpdatedTrack));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Track, action.UpdatedTrack.Id));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new TrackUpdateFailedAction(action.UpdatedTrack.Id));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Track, action.UpdatedTrack.Id, ex.Message, ex.StackTrace));
        }
    }
}
