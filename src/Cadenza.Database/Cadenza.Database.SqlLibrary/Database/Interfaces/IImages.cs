using Cadenza.Database.SqlLibrary.Model.Images;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IImages
{
    Task<GetAlbumArtworkResult> GetAlbumArtwork(int albumId);
    Task<GetArtistImageResult> GetArtistImage(int artistId);
}
