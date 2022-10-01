namespace Cadenza.API.Core;

internal class PlayTrackService : IPlayTrackService
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public PlayTrackService(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    public async Task<List<PlayTrack>> GetPlayTracks()
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.GetAll();
    }

    public async Task<List<PlayTrack>> GetPlayTracksByArtist(string id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.GetByArtist(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByAlbum(string id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.GetByAlbum(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByGenre(string id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.GetByGenre(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByGrouping(Grouping id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.GetByGrouping(id);
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
