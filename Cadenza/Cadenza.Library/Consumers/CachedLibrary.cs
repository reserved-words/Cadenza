namespace Cadenza.Library;

internal class CachedLibrary : ILibrary
{
    private readonly ICacheReader _cache;

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

    public async Task<TrackFull> GetTrack(string trackId)
    {
        return new TrackFull
        {
            Track = _cache.GetTrack(trackId),
            Album = _cache.GetTrackAlbum(trackId),
            Artist = _cache.GetTrackArtist(trackId),
            Position = _cache.GetAlbumPosition(trackId)
        };
    }

    public async Task<TrackSummary> GetTrackSummary(string trackId)
    {
        return new TrackSummary
        {
            Track = _cache.GetTrack(trackId),
            Album = _cache.GetTrackAlbum(trackId),
            Artist = _cache.GetTrackArtist(trackId)
        };
    }
}