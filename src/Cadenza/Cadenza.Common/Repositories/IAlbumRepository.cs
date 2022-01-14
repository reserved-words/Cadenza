using Cadenza.Domain;

namespace Cadenza.Common;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(string id);
}
