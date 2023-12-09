namespace Cadenza.Web.Actions.Effects;

public class EditArtistEffects
{
    private readonly ILibraryApi _api;

    public EditArtistEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchEditArtistRequest(FetchEditArtistRequest action, IDispatcher dispatcher)
    {
        var artist = await _api.GetArtist(action.ArtistId);
        var releases = await _api.GetAlbums(action.ArtistId);
        releases = releases.OrderBy(r => r.ReleaseType).ThenBy(r => r.Year).ToList();
        dispatcher.Dispatch(new FetchEditArtistResult(artist, releases));
    }
}
