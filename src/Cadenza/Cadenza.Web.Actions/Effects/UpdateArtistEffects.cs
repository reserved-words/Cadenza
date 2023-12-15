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
            await _api.UpdateArtist(action.ArtistId, action.UpdatedArtist, action.UpdatedArtistReleases);

            if (action.UpdatedArtist != null)
            {
                dispatcher.Dispatch(new ArtistUpdatedAction(action.ArtistId, action.UpdatedArtist));
            }

            if (action.UpdatedArtistReleases.Any())
            {
                dispatcher.Dispatch(new ArtistReleasesUpdatedAction(action.ArtistId, action.UpdatedArtistReleases));
            }

            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Artist, action.ArtistId));
            dispatcher.Dispatch(new SearchItemsUpdateRequest());
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ArtistUpdateFailedAction(action.ArtistId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Artist, action.ArtistId, ex.Message, ex.StackTrace));
        }
    }
}
