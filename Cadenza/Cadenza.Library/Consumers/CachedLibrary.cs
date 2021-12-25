namespace Cadenza.Library;

internal class CachedLibrary : ILibrary
{
    private readonly ICacheConsumer _cache;

    public CachedLibrary(ICache cache)
    {
        _cache = new CacheReader(cache);
    }

    public async Task<ArtistFull> GetAlbumArtist(string artistId)
    {
        var artist = _cache.GetArtist(artistId);
        if (artist == null)
            return null;

        return new ArtistFull
        {
            Artist = _cache.GetArtist(artistId),
            Albums = _cache.GetArtistAlbums(artistId)
                .Select(a => new AlbumFull
                {
                    Album = a,
                    AlbumTracks = _cache.GetAlbumTracks(a.Id)
                })
                .ToList()
        };
    }

    public async Task<ICollection<Artist>> GetAlbumArtists()
    {
        return _cache.GetAlbumArtists()
            .OfType<Artist>()
            .ToList();
    }

    public async Task<ICollection<Track>> GetAllTracks()
    {
        return _cache.GetAllTracks()
            .OfType<Track>()
            .ToList();
    }

    public async Task<PlayingTrack> GetTrack(string trackId)
    {
        var track = _cache.GetTrack(trackId);
        var album = _cache.GetTrackAlbum(trackId);

        return new PlayingTrack
        {
            Id = trackId,
            Source = track.Source,
            DurationSeconds = track.DurationSeconds,
            Title = track.Title,
            Artist = track.ArtistName,
            AlbumArtist = album.ArtistName,
            AlbumTitle = album.Title,
            ArtworkUrl = album.ArtworkUrl,
            ReleaseType = album.ReleaseType,
            Year = track.Year
        };
    }

    public async Task<FullTrack> GetFullTrack(string trackId)
    {
        var track = _cache.GetTrack(trackId);
        var album = _cache.GetTrackAlbum(trackId);
        var artist = _cache.GetTrackArtist(trackId);
        var position = _cache.GetAlbumPosition(trackId);

        return new FullTrack
        {
            Id = trackId,
            Source = track.Source,
            DurationSeconds = track.DurationSeconds,
            Title = track.Title,
            Artist = track.ArtistName,
            AlbumArtist = album.ArtistName,
            AlbumTitle = album.Title,
            ArtworkUrl = album.ArtworkUrl,
            ReleaseType = album.ReleaseType,
            Year = track.Year,
            DiscNo = position.DiscNo,
            DiscCount = album.DiscCount,
            TrackNo = position.TrackNo,
            TrackCount = album.TrackCounts[position.DiscNo - 1],
            ArtistId = track.ArtistId,
            AlbumArtistId = album.ArtistId,
            AlbumId = track.AlbumId,
            Lyrics = track.Lyrics,
            Tags = track.Tags?.Select(t => t.Value).ToList()
        };
    }
}