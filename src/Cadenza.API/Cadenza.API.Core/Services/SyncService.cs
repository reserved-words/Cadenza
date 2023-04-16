namespace Cadenza.API.Core.Services;

internal class SyncService : ISyncService
{
    private readonly IMusicRepository _musicRepository;
    private readonly IUpdateRepository _updateRepository;

    public SyncService(IMusicRepository musicRepository, IUpdateRepository updateRepository)
    {
        _musicRepository = musicRepository;
        _updateRepository = updateRepository;
    }

    public async Task AddTrack(LibrarySource source, SyncTrack track)
    {
        await _musicRepository.AddTrack(source, track);
    }

    public async Task<List<string>> GetAllTrackSourceIds(LibrarySource source)
    {
        return await _musicRepository.GetAllTracks(source);
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return await _musicRepository.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return await _musicRepository.GetArtistTrackSourceIds(artistId);
    }

    public async Task<SyncSourceTrack> GetTrackIdFromSource(int trackId)
    {
        var idFromSource = await _musicRepository.GetTrackIdFromSource(trackId);
        return new SyncSourceTrack { IdFromSource = idFromSource };
    }

    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        return await _updateRepository.GetUpdateRequests(source);
    }

    public async Task MarkUpdateErrored(ItemUpdateRequest request)
    {
        await _updateRepository.MarkUpdateErrored(request);
    }

    public async Task MarkUpdateDone(ItemUpdateRequest request)
    {
        await _updateRepository.MarkUpdateDone(request);
    }

    public async Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source)
    {
        return await _updateRepository.GetRemovalRequests(source);
    }

    public async Task MarkRemovalErrored(SyncTrackRemovalRequest request)
    {
        await _updateRepository.MarkRemovalErrored(request.RequestId);
    }

    public async Task MarkRemovalDone(SyncTrackRemovalRequest request)
    {
        await _updateRepository.MarkRemovalDone(request.RequestId);
    }

    public async Task RemoveTracks(LibrarySource source, List<string> idsFromSource)
    {
        await _musicRepository.RemoveTracks(idsFromSource);
    }
}
