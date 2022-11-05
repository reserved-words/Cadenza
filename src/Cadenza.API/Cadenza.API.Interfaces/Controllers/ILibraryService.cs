namespace Cadenza.API.Interfaces.Controllers;

public interface ILibraryService
{
    Task<AlbumInfo> Album(string id);
    Task<List<AlbumTrack>> AlbumTracks(string id);
    Task<ArtistInfo> Artist(string id);
    Task<List<Album>> ArtistAlbums(string id);
    Task<List<Track>> ArtistTracks(string id);
    Task<List<Artist>> Artists();
    Task<List<Artist>> GenreArtists(string id);
    Task<List<Artist>> GroupingArtists(Grouping id);
    Task<List<PlayerItem>> Tag(string id);
    Task<TrackFull> Track(string id);
}