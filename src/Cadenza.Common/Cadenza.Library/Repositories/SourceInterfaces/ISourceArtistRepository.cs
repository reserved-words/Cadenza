
namespace Cadenza.Library;

public interface ISourceArtistRepository : IArtistRepository
{
    public LibrarySource Source { get; }
}
