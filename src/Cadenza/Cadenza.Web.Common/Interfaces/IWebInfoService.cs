using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces;

public interface IWebInfoService
{
    Task<string> GetAlbumArtworkUrl(Album album);
    Task<string> GetArtistImageUrl(Artist artist);
}