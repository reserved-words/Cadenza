using Cadenza.API.Common.Model;
using Cadenza.Library;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface IArtistCache : IArtistRepository
{
    Task Populate(FullLibrary library);
}
