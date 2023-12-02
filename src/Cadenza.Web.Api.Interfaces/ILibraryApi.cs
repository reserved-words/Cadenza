namespace Cadenza.Web.Api.Interfaces;

public interface ILibraryApi
{
    Task<AlbumDetailsVM> GetAlbum(int id);
    Task<List<AlbumVM>> GetAlbums(int artistId);
    Task<List<AlbumVM>> GetAlbumsFeaturingArtist(int artistId);
    Task<AlbumTracksVM> GetAlbumTracks(int albumId);
    Task<ArtistDetailsVM> GetArtist(int id);
    Task<List<ArtistVM>> GetArtistsByGenre(string id);
    Task<List<ArtistVM>> GetArtistsByGrouping(int id);
    Task<List<TaggedItemVM>> GetTag(string id);
    Task<TrackFullVM> GetTrack(int id);
}
