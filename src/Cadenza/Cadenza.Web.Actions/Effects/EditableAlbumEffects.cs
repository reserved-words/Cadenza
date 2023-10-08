using Cadenza.Web.State.Actions;

namespace Cadenza.Web.Actions.Effects;

public class EditableAlbumEffects
{
    private readonly IAlbumRepository _repository;

    public EditableAlbumEffects(IAlbumRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchEditableAlbumTracksRequest(FetchEditableAlbumTracksRequest action, IDispatcher dispatcher)
    {
        var result = await _repository.GetAlbumTracks(action.AlbumId);
        dispatcher.Dispatch(new FetchEditableAlbumTracksResultAction(result));
    }
}
