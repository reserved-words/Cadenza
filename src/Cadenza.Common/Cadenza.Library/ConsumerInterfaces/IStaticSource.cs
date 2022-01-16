namespace Cadenza.Library;

public interface IStaticSource
{
    Task<StaticLibrary> GetStaticLibrary();
}
