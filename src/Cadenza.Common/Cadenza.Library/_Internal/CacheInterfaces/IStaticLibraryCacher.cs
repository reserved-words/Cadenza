namespace Cadenza.Library;

internal interface IStaticLibraryCacher
{
    void AddStaticLibrary(StaticLibrary library, bool forceUpdate);
}
