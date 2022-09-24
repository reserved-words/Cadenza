using Cadenza.API.Common.Model;
using Cadenza.Library;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface ISearchCache : ISearchRepository
{
    Task Populate(FullLibrary library);
}