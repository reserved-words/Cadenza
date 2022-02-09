namespace Cadenza.Library;

public class PlayTrackRepository : IPlayTrackRepository
{
    private readonly ILibrary _library;

    private List<PlayTrack> _tracks;
    private Dictionary<string, List<PlayTrack>> _albumTracks;
    private Dictionary<string, List<PlayTrack>> _artistTracks;

    public PlayTrackRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<ListResponse<PlayTrack>> GetAll(int page, int limit)
    {
        return _tracks.ToListResponse(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayTrack>> GetByAlbum(string id, int page, int limit)
    {
        return _albumTracks[id].ToListResponse(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit)
    {
        return _artistTracks[id].ToListResponse(t => t.Id, page, limit);
    }

    public async Task Populate()
    {
        var library = await _library.Get();

        _tracks = library.Tracks
            .Select(t => new PlayTrack
            {
                Id = t.Id,
                Title = t.Title,
                ArtistId = t.ArtistId,
                AlbumId = t.AlbumId,
                Source = t.Source
            })
            .ToList();

        _artistTracks = library.Artists.ToDictionary(a => a.Id, a => new List<PlayTrack>());
        _albumTracks = library.Albums.ToDictionary(a => a.Id, a => new List<PlayTrack>());

        var trackDictionary = new Dictionary<string, PlayTrack>();

        foreach (var track in _tracks)
        {
            trackDictionary.Add(track.Id, track);
            _artistTracks[track.ArtistId].Add(track);
        }

        var albums = library.AlbumTrackLinks
            .GroupBy(a => a.AlbumId);

        foreach (var album in albums)
        {
            _albumTracks[album.Key] = album
                .OrderBy(t => t.Position.DiscNo)
                .ThenBy(t => t.Position.TrackNo)
                .Select(t => trackDictionary[t.TrackId])
                .ToList();
        }
    }
}
