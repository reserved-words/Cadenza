namespace Cadenza.Library;

internal class CacheReader : ICacheConsumer
{
    private readonly ICache _cache;

    public CacheReader(ICache cache)
    {
        _cache = cache;
    }

    public ArtistInfo GetArtist(string artistId) => _cache.Artists.ValueOrDefault(artistId);
    public TrackInfo GetTrack(string trackId) => _cache.Tracks[trackId];

    public ArtistInfo GetTrackArtist(string trackId) => _cache.Artists[_cache.TrackLinks[trackId].ArtistId];
    public AlbumInfo GetTrackAlbum(string trackId) => _cache.Albums[_cache.TrackLinks[trackId].AlbumId];

    public AlbumTrackPosition GetAlbumPosition(string trackId) => _cache.TrackLinks[trackId].Position;

    public ICollection<AlbumInfo> GetArtistAlbums(string artistId)
    {
        return _cache.ArtistLinks[artistId]
            .Albums
            .Select(a => _cache.Albums[a])
            .ToList();
    }

    public ICollection<AlbumTrack> GetAlbumTracks(string albumId)
    {
        return _cache.AlbumLinks[albumId]
               .Tracks
               .Select(t => new AlbumTrack
               {
                   Track = _cache.Tracks[t.TrackId],
                   Position = _cache.TrackLinks[t.TrackId].Position
               })
               .ToList();
    }


    public ICollection<ArtistInfo> GetAlbumArtists()
    {
        return _cache.AlbumArtists
            .Select(a => _cache.Artists[a])
            .ToList();
    }

    public ICollection<TrackInfo> GetAllTracks()
    {
        return _cache.Tracks.Values
            .OfType<TrackInfo>()
            .ToList();
    }
}