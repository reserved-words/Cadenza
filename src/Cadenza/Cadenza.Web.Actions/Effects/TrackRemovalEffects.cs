using Cadenza.Web.State.Actions;

namespace Cadenza.Web.Actions.Effects;

public class TrackRemovalEffects
{
    private readonly IUpdateRepository _repository;

    public TrackRemovalEffects(IUpdateRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleTrackRemovalRequest(TrackRemovalRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.RemoveTrack(action.TrackId);
            dispatcher.Dispatch(new TrackRemovedAction(action.TrackId));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.TrackRemoval, action.TrackId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new TrackRemovalFailedAction(action.TrackId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.TrackRemoval, action.TrackId, ex.Message, ex.StackTrace));
        }
    }
}
