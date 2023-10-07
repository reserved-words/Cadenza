namespace Cadenza.Web.Common.Interfaces;

public interface IWebInfoService
{
    Task<string> GetAlbumArtworkUrl(AlbumVM album);
    Task<string> GetArtistImageUrl(ArtistVM artist);
}