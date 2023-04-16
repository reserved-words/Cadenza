namespace Cadenza.API.Core.Services;

internal class SyncService : ISyncService
{
    private readonly IMusicRepository _repository;
    private readonly IUpdateRepository _updateRepository;

    public SyncService(IMusicRepository repository, IUpdateRepository updateRepository)
    {
        _repository = repository;
        _updateRepository = updateRepository;
    }

    public async Task AddTrack(LibrarySource source, SyncTrack track)
    {
        await _repository.AddTrack(source, track);
    }

    public async Task<List<string>> GetAllTrackSourceIds(LibrarySource source)
    {
        return await _repository.GetAllTracks(source);
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(LibrarySource source, int albumId)
    {
        var library = await _repository.Get(source);

        return library.Tracks
            .Where(t => t.AlbumId == albumId)
            .Select(t => t.IdFromSource)
            .ToList();
    }

    public async Task<List<string>> GetArtistTrackSourceIds(LibrarySource source, int artistId)
    {
        var library = await _repository.Get(source);

        return library.Tracks
            .Where(t => t.ArtistId == artistId)
            .Select(t => t.IdFromSource)
            .ToList();
    }

    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        return await _updateRepository.GetUpdateRequests(source);
    }

    public async Task MarkUpdateErrored(LibrarySource source, ItemUpdateRequest request)
    {
        await _updateRepository.MarkUpdateErrored(request, source);
    }

    public async Task MarkUpdateDone(LibrarySource source, ItemUpdateRequest request)
    {
        await _updateRepository.MarkUpdateDone(request, source);
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
        await _repository.RemoveTracks(idsFromSource);
    }
}
