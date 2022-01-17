namespace Cadenza.Library;

internal class CachedLibrary : ILibrary
{
    private readonly ICacheConsumer _cache;

    public CachedLibrary(ICache cache)
    {
        _cache = new CacheReader(cache);
    }

    public Task<ArtistFull> GetAlbumArtist(string artistId)
    {
        var artist = _cache.GetArtist(artistId);
        if (artist == null)
            return null;

        var result = new ArtistFull
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

        return Task.FromResult(result);
    }

    public Task<ICollection<Artist>> GetAlbumArtists()
    {
        var result = _cache.GetAlbumArtists()
            .OfType<Artist>()
            .ToList();

        return Task.FromResult<ICollection<Artist>>(result);
    }

    public Task<ICollection<Track>> GetAllTracks()
    {
        var result = _cache.GetAllTracks()
            .OfType<Track>()
            .ToList();

        return Task.FromResult<ICollection<Track>>(result);
    }

    public Task<PlayingTrack> GetTrack(string trackId)
    {
        var track = _cache.GetTrack(trackId);
        var album = _cache.GetTrackAlbum(trackId);

        var result = new PlayingTrack
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

        return Task.FromResult(result);
    }

    public Task<FullTrack> GetFullTrack(string trackId)
    {
        var track = _cache.GetTrack(trackId);
        var album = _cache.GetTrackAlbum(trackId);
        var artist = _cache.GetTrackArtist(trackId);
        var position = _cache.GetAlbumPosition(trackId);

        var result = new FullTrack
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
            Tags = track.Tags?.ToList()
        };

        return Task.FromResult(result);
    }
}