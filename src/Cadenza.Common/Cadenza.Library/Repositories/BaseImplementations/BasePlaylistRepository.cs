namespace Cadenza.Library;

public class BasePlaylistRepository : IBasePlaylistRepository
{
    private readonly ILibrary _library;

    private Dictionary<string, Playlist> _playlists;
    private Dictionary<string, List<PlaylistTrack>> _playlistTracks;

    public BasePlaylistRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<Playlist> GetPlaylist(string id)
    {
        return _playlists.TryGetValue(id, out Playlist playlist)
            ? playlist
            : null;
    }

    public async Task<List<PlaylistTrack>> GetPlaylistTracks(string id)
    {
        return _playlistTracks.TryGetValue(id, out List<PlaylistTrack> tracks)
            ? tracks
            : null;
    }

    public async Task Populate()
    {
        if (_playlists != null)
            return;

        var library = await _library.Get();

        _playlists = library.Playlists
            .ToDictionary(a => a.Id, a => a);

        var playlists = library.PlaylistTrackLinks
            .GroupBy(a => a.PlaylistId);

        _playlistTracks = playlists.ToDictionary(a => a.Key, a => new List<PlaylistTrack>());

        var tracks = library.Tracks.ToDictionary(t => t.Id, t => t as Track);

        foreach (var playlist in playlists)
        {
            _playlistTracks[playlist.Key] = playlist
                .OrderBy(t => t.Position)
                .Select(t => GetPlaylistTrack(t.Position, tracks[t.TrackId]))
                .ToList();
        }

    }

    private static PlaylistTrack GetPlaylistTrack(int order, Track track)
    {
        return new PlaylistTrack
        {
            Order = order,
            TrackId = track.Id,
            Title = track.Title,
            ArtistId = track.ArtistId,
            ArtistName = track.ArtistName,
            AlbumId = track.AlbumId,
            DurationSeconds = track.DurationSeconds
        };
    }
}
