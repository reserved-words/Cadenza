namespace Cadenza.Library;

internal interface IStaticLibraryCacher
{
    void MergeStaticLibrary(StaticLibrary baseLibrary, StaticLibrary newLibrary, MergeMode mode);
}
