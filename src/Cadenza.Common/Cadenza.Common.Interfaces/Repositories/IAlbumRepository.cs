using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Common.Interfaces.Repositories;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(int id);
    Task<List<AlbumTrack>> GetAlbumTracks(int albumId);
}
