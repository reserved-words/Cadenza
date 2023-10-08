namespace Cadenza.SyncService.Interfaces;

internal interface ISourceRepository
{
    LibrarySource Source { get; }

    Task<List<string>> GetAllTracks();
    Task<SyncTrackDTO> GetTrack(string idFromSource);
    Task RemoveTrack(SyncTrackRemovalRequestDTO request);
    Task UpdateTracks(List<string> trackIdsFromSource, List<PropertyUpdateDTO> updates);
}
