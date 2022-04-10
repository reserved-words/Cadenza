using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.Local.Common.Interfaces.Cache;

public interface IArtistCache : IArtistRepository
{
    Task Populate(FullLibrary library);
}
