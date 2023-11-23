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

    public async Task AddTrackRemoval(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        await _sql.Execute(parameters);
    }

    public async Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<AlbumUpdateData>(parameters);
    }

    public async Task<List<ArtistUpdateData>> GetArtistUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<ArtistUpdateData>(parameters);
    }

    public async Task<List<TrackRemovalData>> GetTrackRemovals(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<TrackRemovalData>(parameters);
    }

    public async Task<List<TrackUpdateData>> GetTrackUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<TrackUpdateData>(parameters);
    }

    public async Task MarkAlbumUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    public async Task MarkArtistUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    public async Task MarkTrackUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    public async Task MarkTrackRemovalDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    public async Task MarkAlbumUpdateErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    public async Task MarkArtistUpdateErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    public async Task MarkTrackUpdateErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    public async Task MarkTrackRemovalErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute(parameters);
    }

    
}
