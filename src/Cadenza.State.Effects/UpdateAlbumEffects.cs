namespace Cadenza.State.Effects;

public class AlbumUpdateEffects
{
    private readonly IUpdateRepository _repository;

    public AlbumUpdateEffects(IUpdateRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleAlbumUpdateRequest(AlbumUpdateRequest action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateAlbum(action.Update);
            dispatcher.Dispatch(new AlbumUpdatedAction(action.AlbumId, action.Update));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Album, action.AlbumId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new AlbumUpdateFailedAction(action.AlbumId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Album, action.AlbumId, ex.Message, ex.StackTrace));
        }
    }
}
