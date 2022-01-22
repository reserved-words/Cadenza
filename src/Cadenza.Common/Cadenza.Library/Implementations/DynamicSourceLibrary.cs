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

    public async Task<IEnumerable<BasicTrack>> GetAllTracks() => await _baseLibrary.GetAllTracks();

    public async Task<IEnumerable<ArtistInfo>> GetArtists() => await _baseLibrary.GetArtists();

    public async Task<TrackFull> GetFullTrack(string id) => await _baseLibrary.GetFullTrack(id);

    public async Task<TrackSummary> GetTrack(string id) => await _baseLibrary.GetTrack(id);
}
