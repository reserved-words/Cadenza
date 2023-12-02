namespace Cadenza.Web.Api.Interfaces;

public interface IArtistApi
{
    Task<ArtistDetailsVM> GetArtist(int id);
    Task<List<AlbumVM>> GetAlbums(int artistId);
    Task<List<AlbumVM>> GetAlbumsFeaturingArtist(int artistId);
    Task<List<ArtistVM>> GetArtistsByGenre(string id);
    Task<List<ArtistVM>> GetArtistsByGrouping(int id);
}
