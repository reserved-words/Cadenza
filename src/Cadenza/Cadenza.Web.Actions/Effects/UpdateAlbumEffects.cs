namespace Cadenza.Web.Actions.Effects;

public class AlbumUpdateEffects
{
    private readonly IUpdateApi _api;

    public AlbumUpdateEffects(IUpdateApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleAlbumUpdateRequest(AlbumUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _api.UpdateAlbum(action.AlbumId, action.UpdatedAlbum, action.UpdatedAlbumTracks, action.RemovedTracks);

            if (action.UpdatedAlbum != null)
            {
                dispatcher.Dispatch(new AlbumUpdatedAction(action.AlbumId, action.UpdatedAlbum));
            }

            if (action.UpdatedAlbumTracks.Any() || action.RemovedTracks.Any())
            {
                dispatcher.Dispatch(new AlbumTracksUpdatedAction(action.AlbumId, action.UpdatedAlbumTracks, action.RemovedTracks));
            }

            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Album, action.AlbumId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new AlbumUpdateFailedAction(action.AlbumId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Album, action.AlbumId, ex.Message, ex.StackTrace));
        }
    }
}
