namespace Cadenza.Library;

public interface ILibrary
{
    bool IsPopulated { get; }
    Task Populate();
    Task<FullLibrary> Get();
}