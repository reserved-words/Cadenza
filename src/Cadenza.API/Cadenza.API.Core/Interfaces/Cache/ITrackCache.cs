using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface ITrackCache : ITrackRepository
{
    Task Populate(FullLibrary library);
}
