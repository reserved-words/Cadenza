namespace Cadenza.API.Interfaces.Controllers;

public interface ISyncService
{
    Task AddTrack(LibrarySource source, SyncTrack track);
    Task<List<string>> GetAllTrackSourceIds(LibrarySource source);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<SyncSourceTrack> GetTrackIdFromSource(int trackId);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateErrored(ItemUpdateRequest request);
    Task MarkUpdateDone(ItemUpdateRequest request);
    Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalErrored(SyncTrackRemovalRequest request);
    Task MarkRemovalDone(SyncTrackRemovalRequest request);
    Task RemoveTracks(LibrarySource source, List<string> idsFromSource);
}
