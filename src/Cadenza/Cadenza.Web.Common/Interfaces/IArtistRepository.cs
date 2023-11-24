namespace Cadenza.Web.Common.Interfaces;

public interface IArtistRepository
{
    Task<ArtistDetailsVM> GetArtist(int id);
    Task<List<AlbumVM>> GetAlbums(int artistId);
    Task<List<AlbumVM>> GetAlbumsFeaturingArtist(int artistId);
    Task<List<ArtistVM>> GetArtistsByGenre(string id);
    Task<List<ArtistVM>> GetArtistsByGrouping(int id);
}
