using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Queue : IQueue
{
    private readonly ISqlAccess _sql;

    public Queue(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Queue));
    }

    public async Task AddAlbumUpdate(NewAlbumUpdateData data)
    {
        await _sql.Execute(data);
    }

    public async Task AddArtistUpdate(NewArtistUpdateData data)
    {
        await _sql.Execute(data);
    }

    public async Task AddTrackUpdate(NewTrackUpdateData data)
    {
        await _sql.Execute(data);
    }

    public async Task AddTrackRemoval(int id)
    {
        await _sql.Execute(new { Id = id });
    }

    public async Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source)
    {
        return await _sql.Query<AlbumUpdateData>(new { SourceId = (int?)source });
    }

    public async Task<List<ArtistUpdateData>> GetArtistUpdates(LibrarySource source)
    {
        return await _sql.Query<ArtistUpdateData>(new { SourceId = (int)source });
    }

    public async Task<List<TrackRemovalData>> GetTrackRemovals(LibrarySource source)
    {
        return await _sql.Query<TrackRemovalData>(new { SourceId = (int)source });
    }

    public async Task<List<TrackUpdateData>> GetTrackUpdates(LibrarySource source)
    {
        return await _sql.Query<TrackUpdateData>(new { SourceId = (int)source });
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
