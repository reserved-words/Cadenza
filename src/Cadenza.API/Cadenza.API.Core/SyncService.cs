namespace Cadenza.API.Core;
internal class SyncService : ISyncService
{
    private readonly IMusicRepository _repository;
    private readonly IUpdateRepository _updateRepository;

    public SyncService(IMusicRepository repository, IUpdateRepository updateRepository)
    {
        _repository = repository;
        _updateRepository = updateRepository;
    }

    public async Task AddTrack(LibrarySource source, TrackFull track)
    {
        await _repository.AddTrack(source, track);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        var library = await _repository.Get(source);

        return library.Tracks
            .Select(t => t.Id)
            .ToList();
    }

    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        var library = await _repository.Get(source);
        
        return library.Tracks
            .Where(t => t.AlbumId == albumId)
            .Select(t => t.Id)
            .ToList();
    }

    public async Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId)
    {
        var library = await _repository.Get(source);

        return library.Tracks
            .Where(t => t.ArtistId == artistId)
            .Select(t => t.Id)
            .ToList();
    }

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        return await _updateRepository.GetUpdates(source);
    }

    public async Task MarkUpdated(LibrarySource source, ItemUpdates update)
    {
        await _updateRepository.Remove(update, source);
    }

    public async Task RemoveTracks(LibrarySource source, List<string> ids)
    {
        await _repository.RemoveTracks(source, ids);
    }
}
