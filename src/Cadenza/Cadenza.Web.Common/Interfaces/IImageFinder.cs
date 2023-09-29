using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces;

public interface IImageFinder
{
    Task<string> GetBase64ArtworkSource(string url);
    string GetSearchUrl(AlbumInfo model);
    string GetSearchUrl(ArtistInfo model, SearchSource source);
    Task<string> GetUrl(AlbumInfo model);
}