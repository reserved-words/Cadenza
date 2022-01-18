using Cadenza.Domain;

namespace Cadenza.Core;

public interface IPlayTrackRepository
{
    // add more criteria later
    Task<IEnumerable<PlayTrack>> GetAll();
    Task<IEnumerable<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId);
    Task<IEnumerable<PlayTrack>> GetByArtist(string id);
}
