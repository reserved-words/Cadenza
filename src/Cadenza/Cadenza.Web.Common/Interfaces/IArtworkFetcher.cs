namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    Task<string> GetArtistImageUrl(ArtistInfo artist, string trackId = null);
    Task<string> GetArtworkUrl(Album album, string trackId = null);
}
