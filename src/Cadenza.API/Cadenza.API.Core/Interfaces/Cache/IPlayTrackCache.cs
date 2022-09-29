using Cadenza.Common.Domain.Model;
using Cadenza.Common.Interfaces.Repositories;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface IPlayTrackCache : IPlayTrackRepository
{
    Task Populate(FullLibrary library);
}
