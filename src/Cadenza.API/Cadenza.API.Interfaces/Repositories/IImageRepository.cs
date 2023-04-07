namespace Cadenza.API.Interfaces.Repositories;

public interface IImageRepository
{
    Task<ArtworkImage> GetArtistImage(string nameId);
    Task<ArtworkImage> GetAlbumArtwork(int albumId);
}
