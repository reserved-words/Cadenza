namespace Cadenza.Library;

public interface ISourceLibrary : ILibrary
{
    public LibrarySource Source { get; }
}
