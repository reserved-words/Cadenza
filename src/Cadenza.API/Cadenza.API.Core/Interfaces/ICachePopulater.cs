using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Core.Interfaces;

internal interface ICachePopulater
{
    Task Populate(bool onlyIfEmpty);
}
