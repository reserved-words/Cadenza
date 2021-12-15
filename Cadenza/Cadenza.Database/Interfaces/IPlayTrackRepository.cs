using Cadenza.Common;

namespace Cadenza.Database;

public interface IPlayTrackRepository
{
    // add more criteria later
    Task<List<PlayTrack>> GetAll();
    Task<List<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId);
    Task<List<PlayTrack>> GetByArtist(string id);
}

public interface IPlayTrackRepositoryUpdater : IPlayTrackRepository
{
    Task AddAllTracks(LibrarySource source, List<string> tracks);
    Task AddAlbumTracks(LibrarySource source, string albumId, List<string> tracks);
    Task AddArtistTracks(LibrarySource source, string artistId, List<string> tracks);
}