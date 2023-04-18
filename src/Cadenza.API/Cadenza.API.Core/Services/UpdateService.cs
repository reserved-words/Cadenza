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

    public async Task RemoveTrack(TrackRemovalRequest request)
    {
        await _musicRepository.RemoveTrack(request.TrackId);
        await _cachePopulater.Populate(false);
        await _updateRepository.AddRemovalRequest(request);
    }

    public async Task UpdateTrack(ItemUpdateRequest request)
    {
        await _musicRepository.UpdateTrack(request);
        await _cachePopulater.Populate(false);
        await _updateRepository.AddUpdateRequest(request);
    }

    public async Task UpdateAlbum(ItemUpdateRequest request)
    {
        await _musicRepository.UpdateAlbum(request);
        await _cachePopulater.Populate(false);
        await _updateRepository.AddUpdateRequest(request);
    }

    public async Task UpdateArtist(ItemUpdateRequest request)
    {
        await _musicRepository.UpdateArtist(request);
        await _cachePopulater.Populate(false);
        await _updateRepository.AddUpdateRequest(request);
    }
}