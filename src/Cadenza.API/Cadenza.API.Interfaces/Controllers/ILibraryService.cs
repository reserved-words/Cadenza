namespace Cadenza.API.Interfaces.Controllers;

public interface ILibraryService
{
    Task<AlbumInfo> Album(int id);
    Task<List<Album>> AlbumsFeaturingArtist(int id);
    Task<List<AlbumTrack>> AlbumTracks(int id);
    Task<ArtistInfo> Artist(int id);
    Task<List<Album>> ArtistAlbums(int id);
    Task<List<Track>> ArtistTracks(int id);
    Task<List<Artist>> Artists();
    Task<List<Artist>> GenreArtists(string id);
    Task<List<Artist>> GroupingArtists(Grouping id);
    Task<List<PlayerItem>> Tag(string id);
    Task<TrackFull> Track(int id);
}