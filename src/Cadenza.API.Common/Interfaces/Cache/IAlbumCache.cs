using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.API.Common.Interfaces.Cache;

public interface IAlbumCache : IAlbumRepository
{
    Task Populate(FullLibrary library);
    Task UpdateAlbum(AlbumUpdate update);
}
