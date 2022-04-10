using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.Local.Common.Interfaces.Cache;

public interface IPlayTrackCache : IPlayTrackRepository
{
    Task Populate(FullLibrary library);
}
