namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    string GetArtistImageUrl(Artist artist);
    string GetArtworkUrl(Album album);
}
