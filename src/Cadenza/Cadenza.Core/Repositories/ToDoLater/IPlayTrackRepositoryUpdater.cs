using Cadenza.Domain;

namespace Cadenza.Core;

public interface IPlayTrackRepositoryUpdater : IPlayTrackRepository
{
    Task AddAllTracks(LibrarySource source, IEnumerable<string> tracks);
    Task AddAlbumTracks(LibrarySource source, string albumId, IEnumerable<string> tracks);
    Task AddArtistTracks(LibrarySource source, string artistId, IEnumerable<string> tracks);
}