namespace Cadenza.Database.SqlLibrary.Repositories;

internal class LibraryRepository : ILibraryRepository
{
    private readonly ILibraryMapper _mapper;
    private readonly ILibrary _library;

    public LibraryRepository(ILibraryMapper mapper, ILibrary library)
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

    public async Task<AlbumDetailsDTO> GetAlbum(int id)
    {
        var album = await _library.GetAlbum(id);
        var discs = await _library.GetAlbumDiscs(id);
        return _mapper.MapAlbum(album, discs);
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return await _library.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _library.GetTrackSourceIds(source);
    }

    public async Task<ArtistDetailsDTO> GetArtist(int id)
    {
        var artist = await _library.GetArtist(id);
        return _mapper.MapArtist(artist);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return await _library.GetArtistTrackSourceIds(artistId);
    }

    public async Task<List<TaggedItemDTO>> GetTaggedItems(string tag)
    {
        var items = await _library.GetTaggedItems(tag);
        return items.Select(_mapper.MapTaggedItem).ToList();
    }

    public Task<TrackFullDTO> GetTrack(int id)
    {
        throw new NotImplementedException();
        //var track = await _library.GetTrack(id);
        //return _mapper.MapTrack(track);
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
