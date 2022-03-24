namespace Cadenza.Library;

public class AlbumCache : IAlbumCache
{
    private Dictionary<string, AlbumInfo> _albums;
    private Dictionary<string, List<AlbumTrack>> _albumTracks;

    public async Task<AlbumInfo> GetAlbum(string id)
    {
        return _albums.TryGetValue(id, out AlbumInfo album)
            ? album
            : null;
    }

    public async Task<List<AlbumTrack>> GetTracks(string id)
    {
        return _albumTracks.TryGetValue(id, out List<AlbumTrack> tracks)
            ? tracks
            : null;
    }

    public async Task Populate(FullLibrary library)
    {
        _albums = library.Albums.ToDictionary(a => a.Id, a => a);

        var albums = library.AlbumTrackLinks
            .GroupBy(a => a.AlbumId);

        _albumTracks = albums.ToDictionary(a => a.Key, a => new List<AlbumTrack>());

        var tracks = library.Tracks.ToDictionary(t => t.Id, t => t as Track);

        foreach (var album in albums)
        {
            _albumTracks[album.Key] = album
                .OrderBy(t => t.Position.DiscNo)
                .ThenBy(t => t.Position.TrackNo)
                .Select(t => GetAlbumTrack(t.Position, tracks[t.TrackId]))
                .ToList();
        }
    }

    private static AlbumTrack GetAlbumTrack(AlbumTrackPosition position, Track track)
    {
        return new AlbumTrack
        {
            TrackId = track.Id,
            Title = track.Title,
            ArtistId = track.ArtistId,
            ArtistName = track.ArtistName,
            DurationSeconds = track.DurationSeconds,
            Position = position
        };
    }

    public Task UpdateAlbum(AlbumUpdate update)
    {
        if (!_albums.TryGetValue(update.Id, out AlbumInfo album))
            return Task.CompletedTask;

        update.ApplyUpdates(album);

        return Task.CompletedTask;
    }
}
