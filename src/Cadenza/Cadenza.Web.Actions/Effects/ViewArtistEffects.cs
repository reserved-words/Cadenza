using Cadenza.Web.Common.Extensions;

namespace Cadenza.Web.Actions.Effects;

public class ViewArtistEffects
{
    private readonly IArtistApi _api;

    public ViewArtistEffects(IArtistApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewArtistRequest(FetchViewArtistRequest action, IDispatcher dispatcher)
    {
        var artist = await _api.GetArtist(action.ArtistId);
        var albumsByArtist = await _api.GetAlbums(action.ArtistId);
        var albumsFeaturingArtist = await _api.GetAlbumsFeaturingArtist(action.ArtistId);

        var releases = albumsByArtist
            .GroupByReleaseType()
            .AddAlbumsFeaturingArtist(albumsFeaturingArtist);

        dispatcher.Dispatch(new FetchViewArtistResult(artist, releases));
    }
}
