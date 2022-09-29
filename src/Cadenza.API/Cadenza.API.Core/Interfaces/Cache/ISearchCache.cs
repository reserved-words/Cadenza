using Cadenza.Common.Domain.Model;
using Cadenza.Common.Interfaces.Repositories;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface ISearchCache : ISearchRepository
{
    Task Populate(FullLibrary library);
}