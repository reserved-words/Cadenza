using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface IPlayTrackCache : IPlayTrackRepository
{
    Task Populate(FullLibrary library);
}
