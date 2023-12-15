using Cadenza.Database.SqlLibrary.Model.Queue;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IQueue
{
    Task AddAlbumUpdate(int albumId);
    Task AddArtistUpdate(int artistId);
    Task AddTrackRemoval(int trackId);
    Task AddTrackUpdate(int trackId);

    Task<List<GetAlbumUpdatesResult>> GetAlbumUpdates(LibrarySource source);
    Task<List<GetArtistUpdatesResult>> GetArtistUpdates(LibrarySource source);
    Task<List<GetTrackRemovalsResult>> GetTrackRemovals(LibrarySource source);
    Task<List<GetTrackUpdatesResult>> GetTrackUpdates(LibrarySource source);

    Task MarkAlbumUpdateDone(int id);
    Task MarkArtistUpdateDone(int id);
    Task MarkTrackUpdateDone(int id);

    Task MarkAlbumUpdateErrored(int id);
    Task MarkArtistUpdateErrored(int id);
    Task MarkTrackUpdateErrored(int id);

    Task MarkTrackRemovalDone(int id);
    Task MarkTrackRemovalErrored(int id);
}
