using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Common.Controllers;

public interface ILibraryService
{
    Task<AlbumInfo> Album(string id);
    Task<List<Artist>> AlbumArtists();
    Task<List<AlbumTrack>> AlbumTracks(string id);
    Task<ArtistInfo> Artist(string id);
    Task<List<Album>> ArtistAlbums(string id);
    Task<List<Artist>> Artists();
    Task<List<Artist>> GenreArtists(string id);
    Task<List<Artist>> GroupingArtists(Grouping id);
    Task<TrackFull> Track(string id);
    Task<List<Artist>> TrackArtists();
}