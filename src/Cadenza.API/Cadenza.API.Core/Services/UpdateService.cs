using Cadenza.API.Interfaces;

namespace Cadenza.API.Core.Services;

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

    public Task<List<EditedItem>> GetQueuedItemEdits()
    {
        return Task.FromResult(new List<EditedItem>());
    }

    public async Task UpdateTrack(LibrarySource source, EditedItem updates)
    {
        await _updateRepository.Add(updates, source);
        await _musicRepository.UpdateTrack(source, updates);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateAlbum(LibrarySource source, EditedItem updates)
    {
        await _updateRepository.Add(updates, source);
        await _musicRepository.UpdateAlbum(source, updates);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateArtist(EditedItem updates)
    {
        await _updateRepository.Add(updates, null);
        await _musicRepository.UpdateArtist(updates);
        await _cachePopulater.Populate(false);
    }
}