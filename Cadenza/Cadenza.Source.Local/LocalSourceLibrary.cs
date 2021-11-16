namespace Cadenza.Source.Local;

public class LocalSourceLibrary : SourceLibrary
{
    public LocalSourceLibrary(LocalLibrary localLibraryConsumer)
        : base(localLibraryConsumer, Common.Source.Local)
    {

    }
}
