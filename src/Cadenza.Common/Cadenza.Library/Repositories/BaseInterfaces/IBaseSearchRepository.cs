namespace Cadenza.Library;

public interface IBaseSearchRepository : ISearchRepository
{
    Task Populate();
}