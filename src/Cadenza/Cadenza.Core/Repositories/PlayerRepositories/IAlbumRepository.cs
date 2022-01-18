using Cadenza.Domain;

namespace Cadenza.Core;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(string id);
}
