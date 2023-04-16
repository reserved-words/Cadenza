namespace Cadenza.SyncService.Interfaces;

internal interface ISourceRepository
{
    LibrarySource Source { get; }

    Task<List<string>> GetAllTracks();
    Task<SyncTrack> GetTrack(string id);
    Task RemoveTrack(SyncTrackRemovalRequest request);
    Task UpdateTracks(List<string> trackIds, List<PropertyUpdate> updates);
}
