using Cadenza.Database.SqlLibrary.Model.Queue;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IQueue
{
    Task AddAlbumUpdate(AddAlbumUpdateParameter data);
    Task AddArtistUpdate(AddArtistUpdateParameter data);
    Task AddTrackRemoval(int trackId);
    Task AddTrackUpdate(AddTrackUpdateParameter data);

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
