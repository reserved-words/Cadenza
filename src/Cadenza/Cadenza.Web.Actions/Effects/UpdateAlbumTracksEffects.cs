namespace Cadenza.Web.Actions.Effects;

public class UpdateAlbumTracksEffects
{
    private readonly IUpdateApi _api;

    public UpdateAlbumTracksEffects(IUpdateApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleAlbumTracksUpdateRequest(AlbumTracksUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _api.UpdateAlbumTracks(action.AlbumId, action.OriginalTracks, action.UpdatedTracks);
            dispatcher.Dispatch(new AlbumTracksUpdatedAction(action.AlbumId, action.UpdatedTracks));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.AlbumTracks, action.AlbumId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new AlbumTracksUpdateFailedAction(action.AlbumId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.AlbumTracks, action.AlbumId, ex.Message, ex.StackTrace));
        }
    }
}
