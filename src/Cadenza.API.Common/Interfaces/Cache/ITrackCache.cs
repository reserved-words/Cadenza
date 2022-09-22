using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.API.Common.Interfaces.Cache;

public interface ITrackCache : ITrackRepository
{
    Task Populate(FullLibrary library);
}
