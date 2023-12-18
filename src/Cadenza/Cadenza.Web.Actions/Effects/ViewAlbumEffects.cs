namespace Cadenza.Web.Actions.Effects;

public class ViewAlbumEffects
{
    private readonly ILibraryApi _api;

    public ViewAlbumEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewAlbumRequest(FetchViewAlbumRequest action, IDispatcher dispatcher)
    {
        var album = await _api.GetFullAlbum(action.AlbumId);
        dispatcher.Dispatch(new FetchViewAlbumResult(album));
    }
}
