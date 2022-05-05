using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.Local.Common.Interfaces.Cache;

public interface ITrackCache : ITrackRepository
{
    Task Populate(FullLibrary library);
}
