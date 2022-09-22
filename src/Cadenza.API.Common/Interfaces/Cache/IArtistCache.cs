using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.API.Common.Interfaces.Cache;

public interface IArtistCache : IArtistRepository
{
    Task Populate(FullLibrary library);
}
