namespace Cadenza.API.Cache.Services;

internal class CacheService : ICacheService
{
    private readonly IHelperCache _helperCache;
    private readonly IMainCache _mainCache;

    public CacheService(IMainCache mainCache, IHelperCache helperCache)
    {
        _mainCache = mainCache;
        _helperCache = helperCache;
    }

    public Task Populate(FullLibraryDTO library)
    {
        _helperCache.Clear();
        _mainCache.Clear();

        foreach (var artist in library.Artists)
        {
            artist.Genre ??= "None";
            _helperCache.CacheArtist(artist);
            _mainCache.CacheArtist(artist);
        }

        foreach (var album in library.Albums)
        {
            _helperCache.CacheAlbum(album);
            _mainCache.CacheAlbum(album);
        }

        foreach (var track in library.Tracks)
        {
            var album = _mainCache.GetAlbum(track.AlbumId);
            var artist = _mainCache.GetArtist(track.ArtistId);
            _helperCache.CacheTrack(track);
            _mainCache.CacheTrack(track);
        }

        var albumTrackAlbums = library.AlbumTracks
            .GroupBy(at => at.AlbumId);

        foreach (var album in albumTrackAlbums)
        {
            CacheAlbumTracks(album);
        }

        return Task.CompletedTask;
    }

    public Task<AlbumTracksDTO> GetAlbumTracks(int id)
    {
        var result = _helperCache.GetAlbumTracks(id);
        return Task.FromResult(result);
    }

    public Task<List<AlbumDTO>> GetAlbums(int artistId)
    {
        var result = _helperCache.GetAlbumsByArtist(artistId);
        return Task.FromResult(result);
    }

    public Task<List<AlbumDTO>> GetAlbumsFeaturingArtist(int artistId)
    {
        var result = _helperCache.GetAlbumsFeaturingArtist(artistId);
        return Task.FromResult(result);
    }

    public Task<List<ArtistDTO>> GetArtistsByGenre(string id)
    {
        var result = _helperCache.GetArtistsByGenre(id);
        return Task.FromResult(result);
    }

    public Task<List<ArtistDTO>> GetArtistsByGrouping(int id)
    {
        var result = _helperCache.GetArtistsByGrouping(id);
        return Task.FromResult(result);
    }

    public Task<TrackFullDTO> GetTrack(int id)
    {
        var result = _mainCache.GetFullTrack(id);
        return Task.FromResult(result);
    }

    private void CacheAlbumTracks(IGrouping<int, AlbumTrackLinkDTO> albumTracks)
    {
        var album = _mainCache.GetAlbum(albumTracks.Key);

        var tracks = albumTracks
            .OrderBy(at => at.DiscNo)
            .ThenBy(at => at.TrackNo)
            .ToList();

        var discs = new List<AlbumDiscDTO>();

        foreach (var t in tracks)
        {
            var track = _mainCache.GetTrack(t.TrackId);

            if (track.ArtistId != album.ArtistId)
            {
                _helperCache.CacheAlbumFeaturingArtist(track.ArtistId, album);
            }

            _mainCache.CacheAlbumTrack(t);

            var disc = discs.SingleOrDefault(d => d.DiscNo == t.DiscNo);

            if (disc == null)
            {
                disc = new AlbumDiscDTO
                {
                    DiscNo = t.DiscNo,
                    TrackCount = album.DiscTrackCounts[t.DiscNo],
                    Tracks = new List<AlbumTrackDTO>()
                };

                discs.Add(disc);
            }

            disc.Tracks.Add(new AlbumTrackDTO
            {
                TrackId = track.Id,
                Title = track.Title,
                ArtistId = track.ArtistId,
                ArtistName = track.ArtistName,
                DurationSeconds = track.DurationSeconds,
                DiscNo = t.DiscNo,
                TrackNo = t.TrackNo,
                IdFromSource = track.IdFromSource
            });
        }

        var result = new AlbumTracksDTO
        {
            AlbumId = album.Id,
            Discs = discs
        };

        _helperCache.CacheAlbumTracks(result);
    }
}