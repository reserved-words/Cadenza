namespace Cadenza.Library;

public class SourceLibrary : Library, ISourceLibrary
{
    private readonly ISource _source;

    public SourceLibrary(ISourceFactory sourceFactory, IMerger merger, ISource source) 
        : base(sourceFactory, merger)
    {
        _source = source;
    }

    public LibrarySource Source => _source.Source;
}
