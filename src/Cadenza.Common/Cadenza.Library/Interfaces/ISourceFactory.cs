namespace Cadenza.Library;

public interface ISourceFactory
{
    IEnumerable<IStaticLibrary> GetSources();
}