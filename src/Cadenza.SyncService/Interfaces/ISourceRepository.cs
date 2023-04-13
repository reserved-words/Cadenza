namespace Cadenza.SyncService.Interfaces;

internal interface ISourceRepository
{
    LibrarySource Source { get; }

    Task<List<string>> GetAllTracks();
    Task<SyncTrack> GetTrack(string id);
    Task RemoveTrack(RemoveTrackRequest request);
    Task UpdateTracks(List<string> trackIds, List<PropertyUpdate> updates);
}
