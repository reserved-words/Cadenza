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
            await _api.UpdateAlbum(action.OriginalAlbum, action.UpdatedAlbum);
            dispatcher.Dispatch(new AlbumUpdatedAction(action.UpdatedAlbum));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Album, action.OriginalAlbum.Id));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new AlbumUpdateFailedAction(action.OriginalAlbum.Id));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Album, action.OriginalAlbum.Id, ex.Message, ex.StackTrace));
        }
    }
}
