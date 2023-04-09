namespace Cadenza.API.Interfaces.Controllers;

public interface IImageService
{
    Task<ArtworkImage> GetArtistImage(string id);
    Task<ArtworkImage> GetAlbumArtwork(int id);
}
