namespace Cadenza.Library;

public interface IStaticLibrary
{
    Task<FullLibrary> Get();
}