using Cadenza.Domain;

namespace Cadenza.Core;

public interface IArtistRepository
{
    Task<List<LibraryArtist>> GetAlbumArtists();
    Task<LibraryArtistDetails> GetArtist(string id);
}
