using Cadenza.Web.Common.Extensions;
using Cadenza.Web.State.Actions;

namespace Cadenza.Web.Actions.Effects;

public class ViewArtistEffects
{
    private readonly IArtistRepository _repository;

    public ViewArtistEffects(IArtistRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchViewArtistRequest(FetchViewArtistRequest action, IDispatcher dispatcher)
    {
        var artist = await _repository.GetArtist(action.ArtistId);
        var albumsByArtist = await _repository.GetAlbums(action.ArtistId);
        var albumsFeaturingArtist = await _repository.GetAlbumsFeaturingArtist(action.ArtistId);

        var releases = albumsByArtist
            .GroupByReleaseType()
            .AddAlbumsFeaturingArtist(albumsFeaturingArtist);

        dispatcher.Dispatch(new FetchViewArtistResult(artist, releases));
    }
}
