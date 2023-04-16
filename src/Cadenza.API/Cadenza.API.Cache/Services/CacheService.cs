namespace Cadenza.API.Cache.Services;

internal class CacheService : ICacheService
{
    private readonly IItemCache _itemCache;
    private readonly IHelperCache _helperCache;
    private readonly IMainCache _mainCache;
    private readonly IPlayCache _playCache;

    public CacheService(IItemCache itemCache, IMainCache mainCache, IPlayCache playCache, IHelperCache helperCache)
    {
        _itemCache = itemCache;
        _mainCache = mainCache;
        _playCache = playCache;
        _helperCache = helperCache;
    }

    public Task Populate(FullLibrary library)
    {
        _helperCache.Clear();
        _itemCache.Clear();
        _mainCache.Clear();
        _playCache.Clear();

        foreach (var artist in library.Artists)
        {
            artist.Genre ??= "None";
            _helperCache.CacheArtist(artist);
            _itemCache.CacheArtist(artist);
            _mainCache.CacheArtist(artist);
        }

        foreach (var album in library.Albums)
        {
            _helperCache.CacheAlbum(album);
            _itemCache.CacheAllbum(album);
            _mainCache.CacheAlbum(album);
        }

        foreach (var track in library.Tracks)
        {
            var album = _mainCache.GetAlbum(track.AlbumId);
            var artist = _mainCache.GetArtist(track.ArtistId);
            _helperCache.CacheTrack(track);
            _itemCache.CacheTrack(track, album);
            _mainCache.CacheTrack(track);
            _playCache.CacheTrack(track, artist, album);
        }

        var albumTracks = library.AlbumTracks
            .OrderBy(at => at.AlbumId)
            .ThenBy(at => at.DiscNo)
            .ThenBy(at => at.TrackNo)
            .ToList();

        foreach (var albumTrack in albumTracks)
        {
            var track = _mainCache.GetTrack(albumTrack.TrackId);
            _helperCache.CacheAlbumTrack(albumTrack, track);
            _mainCache.CacheAlbumTrack(albumTrack);
        }

        return Task.CompletedTask;
    }

    public Task<AlbumInfo> GetAlbum(int id)
    {
        var result = _mainCache.GetAlbum(id);
        return Task.FromResult(result);
    }

    public Task<List<AlbumTrack>> GetAlbumTracks(int id)
    {
        var result = _helperCache.GetAlbumTracks(id);
        return Task.FromResult(result);
    }

    public Task<List<Album>> GetAlbums(int artistId)
    {
        var result = _helperCache.GetAlbumsByArtist(artistId);
        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetAllArtists()
    {
        var result = _mainCache.GetAllArtists();
        return Task.FromResult(result);
    }

    public Task<ArtistInfo> GetArtist(int id)
    {
        var result = _mainCache.GetArtist(id);
        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetArtistsByGenre(string id)
    {
        var result = _helperCache.GetArtistsByGenre(id);
        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetArtistsByGrouping(Grouping id)
    {
        var result = _helperCache.GetArtistsByGrouping(id);
        return Task.FromResult(result);
    }

    public Task<List<Track>> GetArtistTracks(int artistId)
    {
        var result = _helperCache.GetArtistTracks(artistId);
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchAlbums()
    {
        var result = _itemCache.GetItems(PlayerItemType.Album);
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetArtists()
    {
        var result = _itemCache.GetItems(PlayerItemType.Artist);
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetTags()
    {
        var result = _itemCache.GetTags();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetTracks()
    {
        var result = _itemCache.GetItems(PlayerItemType.Track);
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetGenres()
    {
        var result = _itemCache.GetItems(PlayerItemType.Genre);
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetGroupings()
    {
        var result = _itemCache.GetItems(PlayerItemType.Grouping);
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetTag(string id)
    {
        var result = _itemCache.GetTag(id);
        return Task.FromResult(result);
    }
    public Task<TrackFull> GetTrack(string id)
    {
        var result = _mainCache.GetFullTrack(id);
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayAll()
    {
        var result = _playCache.GetAll();
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayAlbum(int id)
    {
        var result = GetAlbumPlayTracks(id).ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayArtist(int id)
    {
        var result = GetArtistPlayTracks(id).ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayGenre(string id)
    {
        var result = _helperCache.GetArtistsByGenre(id)
            .SelectMany(a => GetArtistPlayTracks(a.Id))
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayGrouping(Grouping id)
    {
        var result = _helperCache.GetArtistsByGrouping(id)
            .SelectMany(a => GetArtistPlayTracks(a.Id))
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayTag(string id)
    {
        var result = _playCache.GetTag(id);
        return Task.FromResult(result);
    }

    private IEnumerable<PlayTrack> GetAlbumPlayTracks(int id)
    {
        return _helperCache.GetAlbumTracks(id).Select(t => _playCache.GetTrack(t.TrackId)).ToList();
    }

    private IEnumerable<PlayTrack> GetArtistPlayTracks(int id)
    {
        return _helperCache.GetArtistTracks(id).Select(t => _playCache.GetTrack(t.Id)).ToList();
    }
}