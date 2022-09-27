using Cadenza.Domain.Model.Album;
using Cadenza.Domain.Model.Track;

namespace Cadenza.Library.Repositories;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(string id);
    Task<List<AlbumTrack>> GetTracks(string albumId);
}
