namespace Cadenza.Library;

internal class StaticSourceManager
{
    private readonly IStaticSource _source;

    private bool _processed;

    public StaticSourceManager(IStaticSource source)
    {
        _source = source;
    }

    public async Task<StaticLibrary> Fetch()
    {
        if (_processed)
            return null;

        var library = await _source.GetStaticLibrary();
        _processed = true;
        return library;
    }
}
