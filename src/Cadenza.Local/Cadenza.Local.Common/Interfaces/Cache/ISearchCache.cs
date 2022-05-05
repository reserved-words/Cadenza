using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.Local.Common.Interfaces.Cache;

public interface ISearchCache : ISearchRepository
{
    Task Populate(FullLibrary library);
}