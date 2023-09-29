using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces;

public interface IImageFinder
{
    Task<string> GetBase64ArtworkSource(string url);
    string GetSearchUrl(AlbumDetails model);
    string GetSearchUrl(ArtistDetails model, SearchSource source);
    Task<string> GetUrl(AlbumDetails model);
}