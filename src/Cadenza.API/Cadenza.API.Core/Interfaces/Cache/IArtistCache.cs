using Cadenza.Domain.Model;
using Cadenza.Library.Repositories;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface IArtistCache : IArtistRepository
{
    Task Populate(FullLibrary library);
}
