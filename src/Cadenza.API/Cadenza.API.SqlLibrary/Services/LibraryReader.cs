using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary.Services;

internal class LibraryReader : ILibraryReader
{
    private readonly IDataMapper _mapper;
    private readonly IDataReadService _readService;

    public LibraryReader(IDataMapper mapper, IDataReadService readService)
    {
        _mapper = mapper;
        _readService = readService;
    }

    public async Task<FullLibrary> Get(LibrarySource? source)
    {
        var artistsData = await _readService.GetArtists();
        var artists = artistsData.Select(a => _mapper.MapArtist(a)).ToList();

        var library = new FullLibrary
        {
            Artists = artists,
            Albums = new List<AlbumInfo>(),
            Tracks = new List<TrackInfo>(),
            AlbumTracks = new List<AlbumTrackLink>()
        };

        if (source.HasValue)
        {
            await AddSource(library, source.Value);
        }
        else
        {
            foreach (var src in Enum.GetValues<LibrarySource>())
            {
                await AddSource(library, src);
            }
        }

        return library;
    }

    public async Task<ArtworkImage> GetAlbumArtwork(int id)
    {
        var data = await _readService.GetAlbumArtwork(id);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _readService.GetAllTrackIds(source);
    }

    public async Task<ArtworkImage> GetArtistImage(int id)
    {
        var data = await _readService.GetArtistImage(id);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }

    private async Task AddSource(FullLibrary library, LibrarySource source)
    {
        var albumsData = await _readService.GetAlbums(source);
        var discsData = await _readService.GetDiscs(source);
        var tracksData = await _readService.GetTracks(source);

        var albums = albumsData.Select(a => _mapper.MapAlbum(a, discsData.Where(d => d.AlbumId == a.Id).ToList())).ToList();
        var albumTracks = tracksData.Select(t => _mapper.MapAlbumTrack(t)).ToList();
        var tracks = tracksData.Select(t => _mapper.MapTrack(t)).ToList();

        library.Albums.AddRange(albums);
        library.Tracks.AddRange(tracks);
        library.AlbumTracks.AddRange(albumTracks);
    }
}