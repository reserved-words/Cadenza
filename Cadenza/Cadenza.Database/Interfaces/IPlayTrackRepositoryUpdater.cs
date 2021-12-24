using Cadenza.Common;

namespace Cadenza.Database;

public interface IPlayTrackRepositoryUpdater : IPlayTrackRepository
{
    Task AddAllTracks(LibrarySource source, List<string> tracks);
    Task AddAlbumTracks(LibrarySource source, string albumId, List<string> tracks);
    Task AddArtistTracks(LibrarySource source, string artistId, List<string> tracks);
}