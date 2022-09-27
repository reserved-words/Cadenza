using Cadenza.Domain.Model;
using Cadenza.Library.Repositories;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface IPlayTrackCache : IPlayTrackRepository
{
    Task Populate(FullLibrary library);
}
