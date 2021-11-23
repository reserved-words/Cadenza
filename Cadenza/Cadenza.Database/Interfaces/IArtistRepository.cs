namespace Cadenza.Database;

public interface IArtistRepository
{
    Task<List<LibraryArtist>> GetAlbumArtists();
    Task<LibraryArtistDetails> GetArtist(string id);
}
