namespace Cadenza.Web.Actions.Effects;

public class UpdateAlbumTracksEffects
{
    private readonly IUpdateRepository _repository;

    public UpdateAlbumTracksEffects(IUpdateRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleAlbumTracksUpdateRequest(AlbumTracksUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateAlbumTracks(action.AlbumId, action.OriginalTracks, action.UpdatedTracks);
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
