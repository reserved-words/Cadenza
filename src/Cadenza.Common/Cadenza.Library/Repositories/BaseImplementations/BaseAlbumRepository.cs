namespace Cadenza.Library;

public class BaseAlbumRepository : IBaseAlbumRepository
{
    private readonly ILibrary _library;

    private Dictionary<string, AlbumInfo> _albums;
    private Dictionary<string, List<Track>> _albumTracks;

    public BaseAlbumRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<AlbumInfo> GetAlbum(string id)
    {
        return _albums[id];
    }

    public async Task<List<Track>> GetTracks(string id)
    {
        return _albumTracks[id];
    }

    public async Task Populate()
    {
        var library = await _library.Get();

        _albums = library.Albums.ToDictionary(a => a.Id, a => a);

        var albums = library.AlbumTrackLinks
            .GroupBy(a => a.AlbumId);

        _albumTracks = albums.ToDictionary(a => a.Key, a => new List<Track>());

        var tracks = library.Tracks.ToDictionary(t => t.Id, t => t as Track);

        foreach (var album in albums)
        {
            _albumTracks[album.Key] = album
                .OrderBy(t => t.Position.DiscNo)
                .ThenBy(t => t.Position.TrackNo)
                .Select(t => tracks[t.TrackId])
                .ToList();
        }
    }
}
