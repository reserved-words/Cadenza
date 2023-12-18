namespace Cadenza.Web.Api.Interfaces;

public interface ILibraryApi
{
    Task<AlbumDetailsVM> GetAlbum(int id);
    Task<List<AlbumVM>> GetAlbums(int artistId);
    Task<List<AlbumVM>> GetAlbumsFeaturingArtist(int artistId);
    Task<ArtistDetailsVM> GetArtist(int id);
    Task<List<ArtistVM>> GetArtistsByGenre(string id);
    Task<List<ArtistVM>> GetArtistsByGrouping(int id);
    Task<AlbumFullVM> GetFullAlbum(int id);
    Task<TrackFullVM> GetFullTrack(int id);
    Task<List<TaggedItemVM>> GetTag(string id);
    Task<TrackDetailsVM> GetTrack(int id);
}
