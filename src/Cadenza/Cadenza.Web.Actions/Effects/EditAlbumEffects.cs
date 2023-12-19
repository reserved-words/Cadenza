namespace Cadenza.Web.Actions.Effects;

public class EditAlbumEffects
{
    private readonly ILibraryApi _api;

    public EditAlbumEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchEditAlbumRequest(FetchEditAlbumRequest action, IDispatcher dispatcher)
    {
        var fullAlbum = await _api.GetFullAlbum(action.AlbumId);

        var album = fullAlbum.Album;
        var tracks = fullAlbum.Discs.SelectMany(d => d.Tracks).ToList();   

        dispatcher.Dispatch(new FetchEditAlbumResult(album, tracks));
    }
}
