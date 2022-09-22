using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.API.Common.Interfaces.Cache;

public interface ISearchCache : ISearchRepository
{
    Task Populate(FullLibrary library);
}