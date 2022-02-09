namespace Cadenza.Library;

public interface ILibrary
{
    Task Populate();
    Task<FullLibrary> Get();
}