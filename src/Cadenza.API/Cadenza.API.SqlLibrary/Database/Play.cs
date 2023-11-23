using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Play : IPlay
{
    private ISqlAccess _sql;

    public Play(ISqlAccess sql)
    {
        _sql = sql;
    }

    public async Task<List<int>> GetAbumTrackIds(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.Query<int>("[Play].[GetAlbumTrackIds]", parameters);
    }


    public async Task<List<int>> GetAllTrackIds()
    {
        return await _sql.Query<int>("[Play].[GetAllTrackIds]");
    }

    public async Task<List<int>> GetArtistTrackIds(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.Query<int>("[Play].[GetArtistTrackIds]", parameters);
    }

    public async Task<List<int>> GetGenreTrackIds(string genre)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Genre", genre);
        return await _sql.Query<int>("[Play].[GetGenreTrackIds]", parameters);
    }

    public async Task<List<int>> GetGroupingTrackIds(int groupingId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", groupingId);
        return await _sql.Query<int>("[Play].[GetGroupingTrackIds]", parameters);
    }

    public async Task<List<int>> GetTagTrackIds(string tag)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Tag", tag);
        return await _sql.Query<int>("[Play].[GetTagTrackIds]", parameters);
    }
}
