
namespace Cadenza.Source.Spotify.Repositories;

internal class SpotifySearchRepository : SearchRepository
{
    public SpotifySearchRepository(ILibrary library)
        :base(library)
    {
    }

    public override LibrarySource Source => LibrarySource.Spotify;
}