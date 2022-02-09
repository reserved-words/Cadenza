namespace Cadenza.Library;

public class TrackRepository : ITrackRepository
{
    private readonly ILibrary _library;

    private Dictionary<string, TrackInfo> _tracks;
    private Dictionary<string, AlbumInfo> _albums;
    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, AlbumTrackLink> _albumTracks;

    public TrackRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        var track = _tracks[id];
        var album = _albums[track.AlbumId];
        var artist = _artists[track.ArtistId];
        var albumTrack = _albumTracks[track.Id];

        return new TrackFull
        {
            Id = track.Id,
            Source = track.Source,
            DurationSeconds = track.DurationSeconds,
            Title = track.Title,
            ArtistId = track.ArtistId,
            Artist = track.ArtistName,
            AlbumId = track.AlbumId,
            AlbumTitle = album.Title,
            AlbumArtistId = album.ArtistId,
            ArtworkUrl = album.ArtworkUrl,
            ReleaseType = album.ReleaseType,
            Year = track.Year,
            Lyrics = track.Lyrics,
            Tags = track.Tags?.ToList() ?? new List<string>(),
            DiscNo = albumTrack.Position.DiscNo,
            DiscCount = album.DiscCount,
            TrackNo = albumTrack.Position.TrackNo,
            TrackCount = album.TrackCounts.Count,
            AlbumYear = album.Year
        };
    }

    public async Task Populate()
    {
        var library = await _library.Get();
        _tracks = library.Tracks.ToDictionary(a => a.Id, a => a);
        _albums = library.Albums.ToDictionary(a => a.Id, a => a);
        _artists = library.Artists.ToDictionary(a => a.Id, a => a);
        _albumTracks = library.AlbumTrackLinks.ToDictionary(a => a.TrackId, a => a);
    }
}
