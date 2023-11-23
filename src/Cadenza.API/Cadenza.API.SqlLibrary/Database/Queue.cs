using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Queue : IQueue
{
    private ISqlAccess _sql;

    public Queue(ISqlAccess sql)
    {
        _sql = sql;
    }

    public async Task AddAlbumUpdate(NewAlbumUpdateData data)
    {
        await _sql.Execute("[Queue].[AddAlbumUpdate]", data);
    }

    public async Task AddArtistUpdate(NewArtistUpdateData data)
    {
        await _sql.Execute("[Queue].[AddArtistUpdate]", data);
    }

    public async Task AddTrackUpdate(NewTrackUpdateData data)
    {
        await _sql.Execute("[Queue].[AddTrackUpdate]", data);
    }

    public async Task AddTrackRemoval(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        await _sql.Execute("[Queue].[AddTrackRemoval]", parameters);
    }

    public async Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<AlbumUpdateData>("[Queue].[GetAlbumUpdates]", parameters);
    }

    public async Task<List<ArtistUpdateData>> GetArtistUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<ArtistUpdateData>("[Queue].[GetArtistUpdates]", parameters);
    }

    public async Task<List<TrackRemovalData>> GetTrackRemovals(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<TrackRemovalData>("[Queue].[GetTrackRemovals]", parameters);
    }

    public async Task<List<TrackUpdateData>> GetTrackUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<TrackUpdateData>("[Queue].[GetTrackUpdates]", parameters);
    }

    public async Task MarkAlbumUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkAlbumUpdateDone]", parameters);
    }

    public async Task MarkArtistUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkArtistUpdateDone]", parameters);
    }

    public async Task MarkTrackUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkTrackUpdateDone]", parameters);
    }

    public async Task MarkTrackRemovalDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkTrackRemovalDone]", parameters);
    }

    public async Task MarkAlbumUpdateErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkAlbumUpdateErrored]", parameters);
    }

    public async Task MarkArtistUpdateErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkArtistUpdateErrored]", parameters);
    }

    public async Task MarkTrackUpdateErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkTrackUpdateErrored]", parameters);
    }

    public async Task MarkTrackRemovalErrored(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        await _sql.Execute("[Queue].[MarkTrackRemovalErrored]", parameters);
    }

    
}
