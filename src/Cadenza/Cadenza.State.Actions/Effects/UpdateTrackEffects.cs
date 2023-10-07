﻿namespace Cadenza.State.Actions.Effects;

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

            dispatcher.Dispatch(new TrackUpdatedAction(updatedTrack));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Track, action.OriginalTrack.Id));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new TrackUpdateFailedAction(action.OriginalTrack.Id));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Track, action.OriginalTrack.Id, ex.Message, ex.StackTrace));
        }
    }
}
