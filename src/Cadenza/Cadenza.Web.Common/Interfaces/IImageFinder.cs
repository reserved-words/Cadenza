namespace Cadenza.Web.Common.Interfaces;

public interface IImageFinder
{
    Task<string> GetBase64ArtworkSource(string url);
    string GetAlbumArtworkSearchUrl(string artist, string title);
    string GetArtistImageSearchUrl(string name, SearchSource source);
    Task<string> GetAlbumArtworkUrl(string artist, string title);
}