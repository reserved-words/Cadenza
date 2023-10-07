namespace Cadenza.Web.Common.Interfaces;

public interface IImageFinder
{
    Task<string> GetBase64ArtworkSource(string url);
    string GetSearchUrl(AlbumDetailsVM model);
    string GetSearchUrl(ArtistDetailsVM model, SearchSource source);
    Task<string> GetUrl(AlbumDetailsVM model);
}