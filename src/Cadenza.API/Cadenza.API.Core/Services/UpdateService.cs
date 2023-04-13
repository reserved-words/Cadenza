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
        await _updateRepository.AddRemovalRequest(request);
        await _musicRepository.RemoveTracks(request.Source, new List<string> { request.TrackId });
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateTrack(LibrarySource source, ItemUpdateRequest request)
    {
        await _updateRepository.AddUpdateRequest(request, source);
        await _musicRepository.UpdateTrack(source, request);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateAlbum(LibrarySource source, ItemUpdateRequest request)
    {
        await _updateRepository.AddUpdateRequest(request, source);
        await _musicRepository.UpdateAlbum(source, request);
        await _cachePopulater.Populate(false);
    }

    public async Task UpdateArtist(ItemUpdateRequest request)
    {
        await _updateRepository.AddUpdateRequest(request, null);
        await _musicRepository.UpdateArtist(request);
        await _cachePopulater.Populate(false);
    }
}