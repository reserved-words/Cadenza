namespace Cadenza.Web.Actions.Effects;

public class UpdateArtistEffects
{
    private readonly IUpdateApi _api;

    public UpdateArtistEffects(IUpdateApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleArtistUpdateRequest(ArtistUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _api.UpdateArtist(action.UpdatedArtist);
            dispatcher.Dispatch(new ArtistUpdatedAction(action.UpdatedArtist));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Artist, action.UpdatedArtist.Id));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ArtistUpdateFailedAction(action.UpdatedArtist.Id));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Artist, action.UpdatedArtist.Id, ex.Message, ex.StackTrace));
        }
    }
}
