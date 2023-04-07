namespace Cadenza.API.Interfaces.Repositories;

public interface IImageRepository
{
    Task<string> GetArtistImage(string nameId);
    Task<string> GetAlbumArtwork(int albumId);
}
