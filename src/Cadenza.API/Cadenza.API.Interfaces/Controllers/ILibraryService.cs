using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Interfaces.Controllers;

public interface ILibraryService
{
    Task<AlbumDetails> Album(int id);
    Task<List<Album>> AlbumsFeaturingArtist(int id);
    Task<List<AlbumTrack>> AlbumTracks(int id);
    Task<ArtistDetails> Artist(int id);
    Task<List<Album>> ArtistAlbums(int id);
    Task<List<Track>> ArtistTracks(int id);
    Task<List<Artist>> Artists();
    Task<List<Artist>> GenreArtists(string id);
    Task<List<Artist>> GroupingArtists(int id);
    Task<List<PlayerItem>> Tag(string id);
    Task<TrackFull> Track(int id);
}