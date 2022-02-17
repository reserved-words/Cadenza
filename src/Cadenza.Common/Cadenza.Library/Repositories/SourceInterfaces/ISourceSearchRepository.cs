namespace Cadenza.Library;

public interface ISourceSearchRepository : ISearchRepository
{
    public LibrarySource Source { get; }
}