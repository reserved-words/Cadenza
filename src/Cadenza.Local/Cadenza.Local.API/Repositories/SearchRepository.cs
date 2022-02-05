using Cadenza.Library;

namespace Cadenza.Local.API;

public class LocalSearchRepository : SearchRepository
{
    public LocalSearchRepository(ILibrary library)
        :base(library)
    {
    }

    public override LibrarySource Source => LibrarySource.Local;
}