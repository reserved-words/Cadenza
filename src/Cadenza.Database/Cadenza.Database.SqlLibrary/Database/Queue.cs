using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Queue;
using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Queue : IQueue
{
    private readonly ISqlAccess _sql;

    public Queue(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Queue));
    }

    public async Task AddAlbumUpdate(int albumId)
    {
        await _sql.Execute(new { AlbumId = albumId });
    }

    public async Task AddArtistUpdate(int artistId)
    {
        await _sql.Execute(new { ArtistId = artistId });
    }

    public async Task AddTrackUpdate(int trackId)
    {
        await _sql.Execute(new { TrackId = trackId });
    }

    public async Task AddTrackRemoval(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task<List<GetAlbumUpdatesResult>> GetAlbumUpdates(LibrarySource source)
    {
        return await _sql.Query<GetAlbumUpdatesResult>(new { SourceId = (int?)source });
    }

    public async Task<List<GetArtistUpdatesResult>> GetArtistUpdates(LibrarySource source)
    {
        return await _sql.Query<GetArtistUpdatesResult>(new { SourceId = (int)source });
    }

    public async Task<List<GetTrackRemovalsResult>> GetTrackRemovals(LibrarySource source)
    {
        return await _sql.Query<GetTrackRemovalsResult>(new { SourceId = (int)source });
    }

    public async Task<List<GetTrackUpdatesResult>> GetTrackUpdates(LibrarySource source)
    {
        return await _sql.Query<GetTrackUpdatesResult>(new { SourceId = (int)source });
    }

    public async Task MarkAlbumUpdateDone(int albumId)
    {
        await _sql.Execute(new { AlbumId = albumId });
    }

    public async Task MarkArtistUpdateDone(int artistId)
    {
        await _sql.Execute(new { ArtistId = artistId });
    }

    public async Task MarkTrackUpdateDone(int trackId)
    {
        await _sql.Execute(new { TrackId = trackId });
    }

    public async Task MarkTrackRemovalDone(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkAlbumUpdateErrored(int albumId)
    {
        await _sql.Execute(new { AlbumId = albumId });
    }

    public async Task MarkArtistUpdateErrored(int artistId)
    {
        await _sql.Execute(new { ArtistId = artistId });
    }

    public async Task MarkTrackUpdateErrored(int trackId)
    {
        await _sql.Execute(new { TrackId = trackId });
    }

    public async Task MarkTrackRemovalErrored(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    
}
