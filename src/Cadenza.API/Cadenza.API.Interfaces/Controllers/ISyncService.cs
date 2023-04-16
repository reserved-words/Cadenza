namespace Cadenza.API.Interfaces.Controllers;

public interface ISyncService
{
    Task AddTrack(LibrarySource source, SyncTrack track);
    Task<List<string>> GetAllTrackSourceIds(LibrarySource source);
    Task<List<string>> GetArtistTrackSourceIds(LibrarySource source, int artistId);
    Task<List<string>> GetAlbumTrackSourceIds(LibrarySource source, int albumId);
    Task<SyncSourceTrack> GetTrackIdFromSource(int trackId);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateErrored(LibrarySource source, ItemUpdateRequest request);
    Task MarkUpdateDone(LibrarySource source, ItemUpdateRequest request);
    Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalErrored(SyncTrackRemovalRequest request);
    Task MarkRemovalDone(SyncTrackRemovalRequest request);
    Task RemoveTracks(LibrarySource source, List<string> idsFromSource);
}
