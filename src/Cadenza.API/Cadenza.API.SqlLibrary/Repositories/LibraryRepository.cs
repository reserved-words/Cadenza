namespace Cadenza.Database.SqlLibrary.Repositories;

internal class LibraryRepository : ILibraryRepository
{
    private readonly IMapper _mapper;
    private readonly ILibrary _library;

    public LibraryRepository(IMapper mapper, ILibrary library)
    {
        _mapper = mapper;
        _library = library;
    }

    public async Task<FullLibraryDTO> Get()
    {
        var artistsData = await _library.GetArtists();
        var artists = artistsData.Select(a => _mapper.MapArtist(a)).ToList();

        var library = new FullLibraryDTO
        {
            Artists = artists
        };

        foreach (var src in Enum.GetValues<LibrarySource>())
        {
            await AddSource(library, src);
        }

        return library;
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return await _library.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _library.GetTrackSourceIds(source);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return await _library.GetArtistTrackSourceIds(artistId);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        return await _library.GetTrackIdFromSource(trackId);
    }

    private async Task AddSource(FullLibraryDTO library, LibrarySource source)
    {
        var albumsData = await _library.GetAlbums(source);
        var discsData = await _library.GetDiscs(source);
        var tracksData = await _library.GetTracks(source);

        var albums = albumsData.Select(a => _mapper.MapAlbum(a, discsData.Where(d => d.AlbumId == a.Id).ToList())).ToList();
        var albumTracks = tracksData.Select(t => _mapper.MapAlbumTrack(t)).ToList();
        var tracks = tracksData.Select(t => _mapper.MapTrack(t)).ToList();

        library.Albums.AddRange(albums);
        library.Tracks.AddRange(tracks);
        library.AlbumTracks.AddRange(albumTracks);
    }
}
