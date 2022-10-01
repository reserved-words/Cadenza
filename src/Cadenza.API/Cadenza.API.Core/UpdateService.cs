namespace Cadenza.API.Core;

internal class UpdateService : IUpdateService
{
    private readonly IMusicRepository _musicRepository;
    private readonly IUpdateRepository _updateRepository;
    private readonly ICachePopulater _cachePopulater;

    public UpdateService(IUpdateRepository updateRepository, IMusicRepository musicRepository, ICachePopulater cachePopulater)
    {
        _updateRepository = updateRepository;
        _musicRepository = musicRepository;
        _cachePopulater = cachePopulater;
    }

    public Task<List<ItemUpdates>> GetQueuedUpdates()
    {
        return Task.FromResult(new List<ItemUpdates>());
    }

    public async Task UpdateTrack(LibrarySource source, ItemUpdates updates)
    {
        await _updateRepository.Add(updates, source);
        await _musicRepository.UpdateTrack(source, updates);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateAlbum(LibrarySource source, ItemUpdates updates)
    {
        await _updateRepository.Add(updates, source);
        await _musicRepository.UpdateAlbum(source, updates);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateArtist(ItemUpdates updates)
    {
        await _updateRepository.Add(updates, null);
        await _musicRepository.UpdateArtist(updates);
        await _cachePopulater.Populate(false);
    }
}