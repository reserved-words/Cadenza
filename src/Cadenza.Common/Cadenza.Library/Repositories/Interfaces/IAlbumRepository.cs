using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Track;

namespace Cadenza.Library;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(string id);
    Task<List<AlbumTrack>> GetTracks(string albumId);
}
