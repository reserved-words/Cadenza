namespace Cadenza.Web.Actions.Effects;

public class EditableAlbumEffects
{
    private readonly IAlbumApi _api;

    public EditableAlbumEffects(IAlbumApi api)
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
