using Cadenza.Web.Common.Extensions;

namespace Cadenza.Web.Actions.Effects;

public class ViewArtistEffects
{
    private readonly IArtistRepository _repository;
    private readonly IArtworkFetcher _artworkFetcher;

    public ViewArtistEffects(IArtistRepository repository, IArtworkFetcher artworkFetcher)
    {
        _repository = repository;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public async Task HandleFetchViewArtistRequest(FetchViewArtistRequest action, IDispatcher dispatcher)
    {
        var artist = await _repository.GetArtist(action.ArtistId);
        var albumsByArtist = await _repository.GetAlbums(action.ArtistId);
        var albumsFeaturingArtist = await _repository.GetAlbumsFeaturingArtist(action.ArtistId);

        foreach (var album in albumsByArtist)
        {
            album.ImageUrl = _artworkFetcher.GetAlbumArtworkSrc(album.Id);
        }

        foreach (var album in albumsFeaturingArtist)
        {
            album.ImageUrl = _artworkFetcher.GetAlbumArtworkSrc(album.Id);
        }

        var releases = albumsByArtist
            .GroupByReleaseType()
            .AddAlbumsFeaturingArtist(albumsFeaturingArtist);

        dispatcher.Dispatch(new FetchViewArtistResult(artist, releases));
    }
}
