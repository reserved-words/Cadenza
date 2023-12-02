namespace Cadenza.Web.Actions.Effects;

public class EditableAlbumEffects
{
    private readonly ILibraryApi _api;

    public EditableAlbumEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchEditableAlbumTracksRequest(FetchEditableAlbumTracksRequest action, IDispatcher dispatcher)
    {
        var result = await _api.GetAlbumTracks(action.AlbumId);
        dispatcher.Dispatch(new FetchEditableAlbumTracksResultAction(result));
    }
}
