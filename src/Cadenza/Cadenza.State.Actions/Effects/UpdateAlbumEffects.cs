namespace Cadenza.State.Actions.Effects;

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

            var updatedAlbum = action.OriginalAlbum with
            {
                Title = action.Update.Title,
                ReleaseType = action.Update.ReleaseType,
                Year = action.Update.Year,
                ArtworkBase64 = action.Update.ArtworkBase64,
                Tags = new ReadOnlyCollection<string>(action.Update.Tags.ToList())
            };

            dispatcher.Dispatch(new AlbumUpdatedAction(action.AlbumId, updatedAlbum));
            dispatcher.Dispatch(new UpdateSucceededAction(UpdateType.Album, action.AlbumId));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new AlbumUpdateFailedAction(action.AlbumId));
            dispatcher.Dispatch(new UpdateFailedAction(UpdateType.Album, action.AlbumId, ex.Message, ex.StackTrace));
        }
    }
}
