using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.Local.Common.Interfaces.Cache;

public interface IAlbumCache : IAlbumRepository
{
    Task Populate(FullLibrary library);
    Task UpdateAlbum(AlbumUpdate update);
}
