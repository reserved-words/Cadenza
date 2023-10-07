namespace Cadenza.API.Interfaces.Controllers;

public interface IWebInfoService
{
    Task<AlbumArtworkDTO> AlbumArtworkUrl(string artist, string title);
    Task<ArtistImageDTO> ArtistImageUrl(string name);
}