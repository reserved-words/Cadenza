namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IQueue
{
    Task AddAlbumUpdate(NewAlbumUpdateData data);
    Task AddArtistUpdate(NewArtistUpdateData data);
    Task AddTrackRemoval(int trackId);
    Task AddTrackUpdate(NewTrackUpdateData data);

    Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source);
    Task<List<ArtistUpdateData>> GetArtistUpdates(LibrarySource source);
    Task<List<TrackRemovalData>> GetTrackRemovals(LibrarySource source);
    Task<List<TrackUpdateData>> GetTrackUpdates(LibrarySource source);

    Task MarkAlbumUpdateDone(int id);
    Task MarkArtistUpdateDone(int id);
    Task MarkTrackUpdateDone(int id);

    Task MarkAlbumUpdateErrored(int id);
    Task MarkArtistUpdateErrored(int id);
    Task MarkTrackUpdateErrored(int id);

    Task MarkTrackRemovalDone(int id);
    Task MarkTrackRemovalErrored(int id);
}
