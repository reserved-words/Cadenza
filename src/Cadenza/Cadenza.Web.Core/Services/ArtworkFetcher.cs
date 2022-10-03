namespace Cadenza.Web.Core.Services;

internal class ArtworkFetcher : IArtworkFetcher
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly List<ISourceArtworkFetcher> _sourceFetchers;

    public ArtworkFetcher(IEnumerable<ISourceArtworkFetcher> sourceFetchers)
    {
        _sourceFetchers = sourceFetchers.ToList();
    }

    public async Task<string> GetArtworkUrl(Album album, string trackId = null)
    {
        if (album != null && album.Id != null && album.ArtworkUrl == null)
        {
            var sourceFetcher = _sourceFetchers.Single(p => p.Source == album.Source);
            album.ArtworkUrl = await sourceFetcher.GetArtworkUrl(album, trackId);
        }

        return album?.ArtworkUrl ?? ArtworkPlaceholderUrl;
    }
}
