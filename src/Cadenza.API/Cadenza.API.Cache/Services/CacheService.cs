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
            _mainCache.CacheAlbum(album);
        }

        foreach (var track in library.Tracks)
        {
            var album = _mainCache.GetAlbum(track.AlbumId);
            var artist = _mainCache.GetArtist(track.ArtistId);
            _mainCache.CacheTrack(track);
        }

        return Task.CompletedTask;
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
}