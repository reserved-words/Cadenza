namespace Cadenza.Library;

public interface ILibrary
{
    LibrarySource Source { get; }
    bool IsPopulated { get; }
    Task Populate();
    Task<FullLibrary> Get();
}