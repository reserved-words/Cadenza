namespace Cadenza.Library;

internal class CacheReader : ICacheReader
{
    private readonly ICache _cache;

    public CacheReader(ICache cache)
    {
        _cache = cache;
    }

    public ArtistInfo GetArtist(string artistId) => _cache.Artists.ValueOrDefault(artistId);
    public AlbumInfo GetAlbum(string albumId) => _cache.Albums[albumId];
    public TrackInfo GetTrack(string trackId) => _cache.Tracks[trackId];

    public ArtistInfo GetTrackArtist(string trackId) => _cache.Artists[_cache.TrackLinks[trackId].ArtistId];
    public AlbumInfo GetTrackAlbum(string trackId) => _cache.Albums[_cache.TrackLinks[trackId].AlbumId];
    public ArtistInfo GetAlbumArtist(string albumId) => _cache.Artists[_cache.AlbumLinks[albumId].ArtistId];

    public AlbumTrackPosition GetAlbumPosition(string trackId) => _cache.TrackLinks[trackId].Position;

    public ICollection<AlbumInfo> GetArtistAlbums(string artistId)
    {
        return _cache.ArtistLinks[artistId]
            .Albums
            .Select(a => _cache.Albums[a])
            .ToList();
    }

    public ICollection<TrackInfo> GetArtistTracks(string artistId)
    {
        return _cache.ArtistLinks[artistId]
            .Tracks
            .Select(t => _cache.Tracks[t])
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