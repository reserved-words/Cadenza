namespace Cadenza.Library;

public class SourceSearchRepository : SearchRepository, ISourceSearchRepository
{

    private readonly ISource _source;

    public SourceSearchRepository(ILibrary library, ISource source)
        : base(library)
    {
        _source = source;
    }

    public LibrarySource Source => _source.Source;
}
