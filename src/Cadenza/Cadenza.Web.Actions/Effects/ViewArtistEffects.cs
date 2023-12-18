using Cadenza.Web.Common.Extensions;

namespace Cadenza.Web.Actions.Effects;

public class ViewArtistEffects
{
    private readonly ILibraryApi _api;

    public ViewArtistEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewArtistRequest(FetchViewArtistRequest action, IDispatcher dispatcher)
    {
        var artist = await _api.GetFullArtist(action.ArtistId, true);

        var releases = artist.Albums
            .GroupByReleaseType()
            .AddAlbumsFeaturingArtist(artist.AlbumsFeaturingArtist);

        dispatcher.Dispatch(new FetchViewArtistResult(artist.Artist, releases));
    }
}
