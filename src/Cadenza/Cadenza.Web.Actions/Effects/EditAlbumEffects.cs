namespace Cadenza.Web.Actions.Effects;

public class EditAlbumEffects
{
    private readonly IUpdateApi _api;

    public EditAlbumEffects(IUpdateApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchEditAlbumRequest(FetchEditAlbumRequest action, IDispatcher dispatcher)
    {
        var album = await _api.GetAlbum(action.AlbumId);
        var tracks = await _api.GetAlbumTracks(action.AlbumId);
        dispatcher.Dispatch(new FetchEditAlbumResult(album, tracks));
    }
}
