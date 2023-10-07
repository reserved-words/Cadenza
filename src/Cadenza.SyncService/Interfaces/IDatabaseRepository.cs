namespace Cadenza.SyncService.Interfaces;

internal interface IDatabaseRepository
{
    Task AddTrack(LibrarySource source, SyncTrackDTO track);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalErrored(SyncTrackRemovalRequestDTO request);
    Task MarkRemovalDone(SyncTrackRemovalRequestDTO request);
    Task<List<string>> GetTracksByArtist(int artistId);
    Task<List<string>> GetTracksByAlbum(int albumId);
    Task<SyncSourceTrackDTO> GetTrackIdFromSource(int trackId);
    Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateErrored(ItemUpdateRequestDTO request);
    Task MarkUpdateDone(ItemUpdateRequestDTO request);
    Task RemoveTracks(LibrarySource source, List<string> idsFromSource);
}