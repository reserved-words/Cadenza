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

    public async Task RemoveTrack(TrackRemovalRequestDTO request)
    {
        await _updateRepository.AddRemovalRequest(request);
        await _musicRepository.RemoveTrack(request.TrackId);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateTrack(ItemUpdateRequestDTO request)
    {
        await _updateRepository.AddUpdateRequest(request);
        await _musicRepository.UpdateTrack(request);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateAlbum(ItemUpdateRequestDTO request)
    {
        await _updateRepository.AddUpdateRequest(request);
        await _musicRepository.UpdateAlbum(request);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateArtist(ItemUpdateRequestDTO request)
    {
        await _updateRepository.AddUpdateRequest(request);
        await _musicRepository.UpdateArtist(request);
        await _cachePopulater.Populate(false);
    }
}