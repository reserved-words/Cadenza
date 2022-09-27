using Cadenza.Domain.Model;
using Cadenza.Library.Repositories;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface ISearchCache : ISearchRepository
{
    Task Populate(FullLibrary library);
}