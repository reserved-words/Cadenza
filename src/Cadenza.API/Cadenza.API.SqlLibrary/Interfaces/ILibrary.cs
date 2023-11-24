using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface ILibrary
{
    Task<List<GetArtistsResult>> GetArtists();
    Task<List<GetAlbumsResult>> GetAlbums(LibrarySource? source);
    Task<List<GetDiscsResult>> GetDiscs(LibrarySource? source);
    Task<List<GetTracksResult>> GetTracks(LibrarySource? source);
    Task<List<string>> GetTrackSourceIds(LibrarySource source);

    Task<GetAlbumResult> GetAlbum(int albumId);
    Task<GetArtistResult> GetArtist(int artistId);
    Task<GetTrackResult> GetTrack(int trackId);

    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<string> GetTrackIdFromSource(int trackId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
}
