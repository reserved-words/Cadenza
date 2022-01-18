namespace Cadenza.Library;

internal interface IStaticLibraryCacher
{
    void AddStaticLibrary(StaticLibrary baseLibrary, StaticLibrary newLibrary, bool forceUpdate);
}
