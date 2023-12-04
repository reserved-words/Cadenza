namespace Cadenza.Web.Actions.Effects;

public class ViewGenreEffects
{
    private readonly ILibraryApi _api;

    public ViewGenreEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewGenreRequest(FetchViewGenreRequest action, IDispatcher dispatcher)
    {
        var artists = await _api.GetArtistsByGenre(action.Genre);
        var orderedArtists = artists.OrderBy(a => a.Name).ToList();
        dispatcher.Dispatch(new FetchViewGenreResult(action.Genre, orderedArtists));
    }
}
