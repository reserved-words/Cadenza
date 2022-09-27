using Cadenza.Domain.Model;
using Cadenza.Library.Repositories;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface IAlbumCache : IAlbumRepository
{
    Task Populate(FullLibrary library);
}
