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

    public async Task AddAlbumUpdate(AddAlbumUpdateParameter data)
    {
        await _sql.Execute(data);
    }

    public async Task AddArtistUpdate(AddArtistUpdateParameter data)
    {
        await _sql.Execute(data);
    }

    public async Task AddTrackUpdate(AddTrackUpdateParameter data)
    {
        await _sql.Execute(data);
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

    public async Task MarkAlbumUpdateDone(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkArtistUpdateDone(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkTrackUpdateDone(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkTrackRemovalDone(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkAlbumUpdateErrored(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkArtistUpdateErrored(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkTrackUpdateErrored(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task MarkTrackRemovalErrored(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    
}
