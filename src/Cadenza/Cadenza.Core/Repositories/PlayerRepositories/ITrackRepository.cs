using Cadenza.Domain;

namespace Cadenza.Core;

public interface ITrackRepository
{
    Task<TrackSummary> GetSummary(LibrarySource source, string id);
    Task<TrackFull> GetDetails(LibrarySource source, string id);
    Task<List<AlbumTrackInfo>> GetAlbumTracks(LibrarySource source, string id);
}
