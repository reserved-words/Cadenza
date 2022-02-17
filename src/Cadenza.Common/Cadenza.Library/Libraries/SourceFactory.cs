namespace Cadenza.Library;

public class SourceFactory : ISourceFactory
{
    private readonly IEnumerable<IStaticLibrary> _sources;

    public SourceFactory(IEnumerable<IStaticLibrary> sources)
    {
        _sources = sources;
    }

    public IEnumerable<IStaticLibrary> GetSources()
    {
        return _sources;
    }
}
