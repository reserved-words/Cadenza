namespace Cadenza.Library;

internal class Cache : ILibrary
{
    private Dictionary<string, ArtistInfo> _artists { get; }
    private Dictionary<string, TrackInfo> _tracks { get; }
    private Dictionary<string, AlbumInfo> _albums { get; }
    private Dictionary<string, AlbumTrackLink> _links { get; }

    public Cache(StaticLibrary source)
    {
        _artists = source.Artists.ToDictionary(a => a.Id, a => a);
        _albums = source.Albums.ToDictionary(a => a.Id, a => a);
        _tracks = source.Tracks.ToDictionary(t => t.Id, t => t);
        _links = source.AlbumTrackLinks.ToDictionary(at => at.TrackId, at => at);
    }

    public Task<IEnumerable<AlbumInfo>> GetAlbums()
    {
        return Task.FromResult(_albums.Values.OfType<AlbumInfo>());
    }

    public Task<IEnumerable<BasicTrack>> GetAllTracks()
    {
        return Task.FromResult(_tracks.Values.OfType<TrackInfo>().Select(t => GetBasicTrack(t)));
    }

    public Task<IEnumerable<ArtistInfo>> GetArtists()
    {
        return Task.FromResult(_artists.Values.OfType<ArtistInfo>());
    }

    public Task<TrackFull> GetFullTrack(string id)
    {
        var track = _tracks[id];
        var album = _albums[track.AlbumId];
        var link = _links[id];

        var result = new TrackFull
        {
            Id = track.Id,
            Title = track.Title,
            ArtistId = track.ArtistId,
            Artist = track.ArtistName,
            AlbumId = album.Id,
            AlbumArtist = album.ArtistName,
            AlbumArtistId = album.ArtistId,
            AlbumTitle = album.Title,
            AlbumYear = album.Year,
            ArtworkUrl = album.ArtworkUrl,
            DurationSeconds = track.DurationSeconds,
            ReleaseType = album.ReleaseType,
            Source = track.Source,
            Year = track.Year,
            TrackNo = link.Position.TrackNo,
            TrackCount = album.TrackCounts[link.Position.DiscNo - 1],
            DiscCount = album.DiscCount,
            DiscNo = link.Position.DiscNo,
            Lyrics = track.Lyrics,
            Tags = track.Tags?.ToList() ?? new List<string>()
        };

        return Task.FromResult(result);
    }

    public Task<TrackSummary> GetTrack(string id)
    {
        var track = _tracks[id];
        var album = _albums[track.AlbumId];

        var result = new TrackSummary
        {
            Id = track.Id,
            Title = track.Title,
            Artist = track.ArtistName,
            AlbumArtist = album.ArtistName,
            AlbumTitle = album.Title,
            ArtworkUrl = album.ArtworkUrl,
            DurationSeconds = track.DurationSeconds,
            ReleaseType = album.ReleaseType,
            Source = track.Source,
            Year = track.Year
        };

        return Task.FromResult(result);
    }

    private BasicTrack GetBasicTrack(TrackInfo track)
    {
        var albumId = track.AlbumId ?? _links[track.Id].AlbumId;
        var album = _albums[albumId];

        return new BasicTrack
        {
            Source = track.Source,
            Id = track.Id,
            Title = track.Title,
            ArtistId = track.ArtistId,
            AlbumId = track.AlbumId
        };
    }
}