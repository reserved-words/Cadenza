namespace Cadenza.API.Interfaces.Controllers;

public interface ISyncService
{
    Task AddTrack(LibrarySource source, SyncTrackDTO track);
    Task<List<string>> GetAllTrackSourceIds(LibrarySource source);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<SyncSourceTrackDTO> GetTrackIdFromSource(int trackId);
    Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateErrored(ItemUpdateRequestDTO request);
    Task MarkUpdateDone(ItemUpdateRequestDTO request);
    Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalErrored(SyncTrackRemovalRequestDTO request);
    Task MarkRemovalDone(SyncTrackRemovalRequestDTO request);
    Task RemoveTracks(LibrarySource source, List<string> idsFromSource);
}
