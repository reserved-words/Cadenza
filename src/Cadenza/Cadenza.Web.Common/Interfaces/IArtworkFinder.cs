namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFinder
{
    Task<string> GetBase64ArtworkSource(string url);
    string GetSearchUrl(AlbumInfo model);
    Task<string> GetUrl(AlbumInfo model);
}