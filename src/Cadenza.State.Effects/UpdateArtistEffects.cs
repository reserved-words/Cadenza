namespace Cadenza.State.Effects;

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
            dispatcher.Dispatch(new ArtistUpdatedAction(action.ArtistId, action.Update));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Artist, action.ArtistId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ArtistUpdateFailedAction(action.ArtistId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Artist, action.ArtistId, ex.Message, ex.StackTrace));
        }
    }
}
