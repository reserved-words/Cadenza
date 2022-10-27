namespace Cadenza.API.Core.Interfaces.Cache;

internal interface ISearchCache : ISearchRepository, ITagRepository
{
    Task Populate(FullLibrary library);
}