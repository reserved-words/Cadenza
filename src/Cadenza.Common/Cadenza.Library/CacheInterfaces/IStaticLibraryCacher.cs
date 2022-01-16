namespace Cadenza.Library;

public interface IStaticLibraryCacher
{
    void AddStaticLibrary(StaticLibrary library, bool forceUpdate);
}
