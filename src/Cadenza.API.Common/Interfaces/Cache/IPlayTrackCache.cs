using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.API.Common.Interfaces.Cache;

public interface IPlayTrackCache : IPlayTrackRepository
{
    Task Populate(FullLibrary library);
}
