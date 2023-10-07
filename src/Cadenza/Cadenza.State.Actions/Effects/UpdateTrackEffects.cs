namespace Cadenza.State.Actions.Effects;

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

            var updatedTrack = action.OriginalTrack with
            {
                Title = action.Update.Title,
                Year = action.Update.Year,
                Lyrics = action.Update.Lyrics,
                Tags = new ReadOnlyCollection<string>(action.Update.Tags.ToList())
            };

            dispatcher.Dispatch(new TrackUpdatedAction(action.TrackId, updatedTrack));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Track, action.TrackId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new TrackUpdateFailedAction(action.TrackId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Track, action.TrackId, ex.Message, ex.StackTrace));
        }
    }
}
