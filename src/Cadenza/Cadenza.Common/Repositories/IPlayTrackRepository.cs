using Cadenza.Domain;

namespace Cadenza.Common;

public interface IPlayTrackRepository
{
    // add more criteria later
    Task<List<PlayTrack>> GetAll();
    Task<List<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId);
    Task<List<PlayTrack>> GetByArtist(string id);
}
