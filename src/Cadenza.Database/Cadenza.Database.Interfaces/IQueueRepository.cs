namespace Cadenza.Database.Interfaces;

public interface IQueueRepository
{
    Task AddArtistUpdateRequest(int artistId);
    Task<List<ArtistUpdateSyncDTO>> GetArtistUpdateRequests(LibrarySource source);
    Task MarkArtistUpdateDone(int artistId);
    Task MarkArtistUpdateErrored(int artistId);

    Task AddAlbumUpdateRequest(int albumId);
    Task<List<AlbumUpdateSyncDTO>> GetAlbumUpdateRequests(LibrarySource source);
    Task MarkAlbumUpdateDone(int albumId);
    Task MarkAlbumUpdateErrored(int albumId);

    Task AddTrackUpdateRequest(int trackId);
    Task<List<TrackUpdateSyncDTO>> GetTrackUpdateRequests(LibrarySource source);
    Task MarkTrackUpdateDone(int trackId);
    Task MarkTrackUpdateErrored(int trackId);

    Task AddRemovalRequest(int trackId);
    Task<List<TrackRemovalSyncDTO>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalDone(int requestId);
    Task MarkRemovalErrored(int requestId);
}
