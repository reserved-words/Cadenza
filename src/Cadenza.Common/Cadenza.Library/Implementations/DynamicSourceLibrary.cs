namespace Cadenza.Library;

public class DynamicSourceLibrary : ISourceLibrary
{
    private readonly LibrarySource _librarySource;
    private readonly ILibrary _baseLibrary;

    internal DynamicSourceLibrary(LibrarySource librarySource, ILibrary baseLibrary)
    {
        _baseLibrary = baseLibrary;
        _librarySource = librarySource;
    }

    public LibrarySource Source => _librarySource;

    public async Task<IEnumerable<AlbumInfo>> GetAlbums() => await _baseLibrary.GetAlbums();

    public async Task<IEnumerable<string>> GetAlbumTracks(string artistId, string albumId) => await _baseLibrary.GetAlbumTracks(artistId, albumId);

    public async Task<IEnumerable<string>> GetAllTracks() => await _baseLibrary.GetAllTracks();

    public async Task<IEnumerable<ArtistInfo>> GetArtists() => await _baseLibrary.GetArtists();

    public async Task<IEnumerable<string>> GetArtistTracks(string id) => await _baseLibrary.GetArtistTracks(id);

    public async Task<FullTrack> GetFullTrack(string id) => await _baseLibrary.GetFullTrack(id);

    public async Task<PlayingTrack> GetTrack(string id) => await _baseLibrary.GetTrack(id);
}
