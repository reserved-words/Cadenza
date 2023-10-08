namespace Cadenza.Web.Actions.Effects;

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
            await _repository.UpdateArtist(action.OriginalArtist, action.UpdatedArtist);
            dispatcher.Dispatch(new ArtistUpdatedAction(action.UpdatedArtist));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Artist, action.OriginalArtist.Id));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ArtistUpdateFailedAction(action.OriginalArtist.Id));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Artist, action.OriginalArtist.Id, ex.Message, ex.StackTrace));
        }
    }
}
