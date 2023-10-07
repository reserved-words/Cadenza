﻿namespace Cadenza.State.Actions.Effects;

public class UpdateArtistEffects
{
    private readonly IUpdateRepository _repository;

    public UpdateArtistEffects(IUpdateRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleArtistUpdateRequest(ArtistUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateArtist(action.Update);

            var updatedArtist = action.OriginalArtist with
            {
                Grouping = action.Update.Grouping,
                Genre = action.Update.Genre,
                ImageBase64 = action.Update.ImageBase64,
                Country = action.Update.Country,
                State = action.Update.State,
                City = action.Update.City,
                Tags = new ReadOnlyCollection<string>(action.Update.Tags.ToList())
            };

            dispatcher.Dispatch(new ArtistUpdatedAction(updatedArtist));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Artist, action.OriginalArtist.Id));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ArtistUpdateFailedAction(action.OriginalArtist.Id));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Artist, action.OriginalArtist.Id, ex.Message, ex.StackTrace));
        }
    }
}
