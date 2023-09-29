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

    public async Task<List<int>> GetPlayTracks()
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayAll();
    }

    public async Task<List<int>> GetPlayTracksByAlbum(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayAlbum(id);
    }

    public async Task<List<int>> GetPlayTracksByArtist(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayArtist(id);
    }

    public async Task<List<int>> GetPlayTracksByGenre(string id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayGenre(id);
    }

    public async Task<List<int>> GetPlayTracksByGrouping(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayGrouping(id);
    }

    public async Task<List<int>> GetPlayTracksByTag(string id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayTag(id);
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
