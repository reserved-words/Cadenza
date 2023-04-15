namespace Cadenza.API.Interfaces.Controllers;

public interface ISyncService
{
    Task AddTrack(LibrarySource source, SyncTrack track);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId);
    Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateErrored(LibrarySource source, ItemUpdateRequest request);
    Task MarkUpdateDone(LibrarySource source, ItemUpdateRequest request);
    Task<List<TrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalErrored(TrackRemovalRequest request);
    Task MarkRemovalDone(TrackRemovalRequest request);
    Task RemoveTracks(LibrarySource source, List<string> ids);
}
