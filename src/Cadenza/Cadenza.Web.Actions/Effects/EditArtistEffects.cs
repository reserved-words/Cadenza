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
        var artist = await _api.GetFullArtist(action.ArtistId, false);
        var releases = artist.Albums.OrderBy(r => r.ReleaseType).ThenBy(r => r.Year).ToList();
        dispatcher.Dispatch(new FetchEditArtistResult(artist.Artist, releases));
    }
}
