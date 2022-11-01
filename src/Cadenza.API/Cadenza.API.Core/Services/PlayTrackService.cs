using Cadenza.API.Interfaces;

namespace Cadenza.API.Core.Services;

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
        return await _cache.PlayTrackCache.PlayAll();
    }

    public async Task<List<PlayTrack>> GetPlayTracksByAlbum(string id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.PlayAlbum(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByArtist(string id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.PlayArtist(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByGenre(string id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.PlayGenre(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByGrouping(Grouping id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.PlayGrouping(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByTag(string id)
    {
        await PopulateCache();
        return await _cache.PlayTrackCache.PlayTag(id);
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
