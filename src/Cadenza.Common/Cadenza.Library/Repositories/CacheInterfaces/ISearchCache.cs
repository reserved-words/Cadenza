namespace Cadenza.Library;

public interface ISearchCache : ISearchRepository
{
    Task Populate();
}