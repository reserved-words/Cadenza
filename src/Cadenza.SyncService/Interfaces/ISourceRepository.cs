namespace Cadenza.SyncService.Interfaces;

internal interface ISourceRepository
{
    LibrarySource Source { get; }

    Task<List<string>> GetAllTracks();
    Task<SyncTrack> GetTrack(string idFromSource);
    Task RemoveTrack(SyncTrackRemovalRequest request);
    Task UpdateTracks(List<string> trackIdsFromSource, List<PropertyUpdate> updates);
}
