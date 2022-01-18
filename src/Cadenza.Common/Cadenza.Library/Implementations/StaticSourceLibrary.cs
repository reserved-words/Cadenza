namespace Cadenza.Library;

public class StaticSourceLibrary : Library, ISourceLibrary
{
    private readonly LibrarySource _librarySource;

    internal StaticSourceLibrary(LibrarySource librarySource, IStaticLibraryCacher combiner, IStaticSource source, IStaticSource overrides = null)
        :base(combiner, source, overrides)
    {
        _librarySource = librarySource;
    }

    public LibrarySource Source => _librarySource;
}
