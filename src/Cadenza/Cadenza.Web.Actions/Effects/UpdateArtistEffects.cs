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
            await _api.UpdateArtist(action.OriginalArtist, action.UpdatedArtist);
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
