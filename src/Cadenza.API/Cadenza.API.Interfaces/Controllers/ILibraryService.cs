using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.API.Interfaces.Controllers;

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