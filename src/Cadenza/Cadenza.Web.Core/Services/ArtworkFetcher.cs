using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.Web.Core.Services;

internal class ArtworkFetcher : IArtworkFetcher
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly List<ISourceArtworkFetcher> _sourceFetchers;

    public ArtworkFetcher(IEnumerable<ISourceArtworkFetcher> sourceFetchers)
    {
        _sourceFetchers = sourceFetchers.ToList();
    }

    public async Task<string> GetArtistImageUrl(ArtistInfo artist, string trackId = null)
    {
        if (artist != null && artist.ImageUrl == null)
        {
            artist.ImageUrl = $"https://localhost:56457/api/Image/Artist/{artist.Id}";
        }

        //if (artist != null && artist.Id != null && artist.ImageUrl == null)
        //{
        //    foreach (var sourceFetcher in _sourceFetchers)
        //    {
        //        var imageUrl = await sourceFetcher.GetArtistImageUrl(artist, trackId);

        //        if (imageUrl != null)
        //        {
        //            artist.ImageUrl = imageUrl;
        //            break;
        //        }
        //    }
        //}

        return artist?.ImageUrl ?? ArtworkPlaceholderUrl;
    }

    public async Task<string> GetArtworkUrl(Album album, string trackId = null)
    {
        if (album != null && album.ArtworkUrl == null)
        {
            album.ArtworkUrl = $"https://localhost:56457/api/Image/Album/{album.Id}";
        }

        //if (album != null && album.Id != null && album.ArtworkUrl == null)
        //{
        //    var sourceFetcher = _sourceFetchers.Single(p => p.Source == album.Source);
        //    album.ArtworkUrl = await sourceFetcher.GetArtworkUrl(album, trackId);
        //}

        return album?.ArtworkUrl ?? ArtworkPlaceholderUrl;
    }
}
