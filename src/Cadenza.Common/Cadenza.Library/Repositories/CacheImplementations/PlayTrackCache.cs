namespace Cadenza.Library;

public class PlayTrackCache : IPlayTrackCache
{
    private List<PlayTrack> _tracks;
    private Dictionary<string, List<PlayTrack>> _albumTracks;
    private Dictionary<string, List<PlayTrack>> _artistTracks;
    private Dictionary<Grouping, List<string>> _groupingArtists;
    private Dictionary<string, List<string>> _genreArtists;

    public async Task<ListResponse<PlayTrack>> GetAll(int page, int limit)
    {
        return _tracks.ToListResponse(t => t.Id, page, limit);
    }

    public async Task<List<PlayTrack>> GetByAlbum(string id)
    {
        return _albumTracks.TryGetValue(id, out List<PlayTrack> tracks)
            ? tracks
            : new List<PlayTrack>();
    }

    public async Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit)
    {
        return _artistTracks.TryGetValue(id, out List<PlayTrack> tracks)
            ? tracks.ToListResponse(t => t.Id, page, limit)
            : ListResponse<PlayTrack>.Empty;
    }

    public async Task<ListResponse<PlayTrack>> GetByGenre(string id, int page, int limit)
    {
        var genreArtists = _genreArtists.TryGetValue(id, out List<string> artistIds)
            ? artistIds
            : new List<string>();

        return genreArtists
            .SelectMany(a => _artistTracks[a])
            .ToListResponse(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayTrack>> GetByGrouping(Grouping id, int page, int limit)
    {
        var groupingArtists = _groupingArtists.TryGetValue(id, out List<string> artistIds)
            ? artistIds
            : new List<string>();

        return groupingArtists
            .SelectMany(a => _artistTracks[a])
            .ToListResponse(t => t.Id, page, limit);
    }

    public async Task Populate(FullLibrary library)
    {
        _groupingArtists = library.Artists
            .GroupBy(a => a.Grouping)
            .ToDictionary(g => g.Key, g => g.Select(a => a.Id).ToList());

        _genreArtists = library.Artists
            .GroupBy(a => a.Genre ?? "None")
            .ToDictionary(g => g.Key, g => g.Select(a => a.Id).ToList());

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
