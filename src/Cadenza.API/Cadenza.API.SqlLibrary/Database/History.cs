using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class History : IHistory
{
    private ISqlAccess _sql;

    public History(ISqlAccess sql)
    {
        _sql = sql;
    }

    public async Task<List<RecentAlbumData>> GetRecentAlbums(int maxItems)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@MaxItems", maxItems);
        return await _sql.Query<RecentAlbumData>("[History].[GetRecentAlbums]", parameters);
    }

    public async Task<List<RecentTagData>> GetRecentTags(int maxItems)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@MaxItems", maxItems);
        return await _sql.Query<RecentTagData>("[History].[GetRecentTags]", parameters);
    }

    public async Task LogAlbumPlay(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@AlbumId", albumId);
        await _sql.Execute("[History].[LogAlbumPlay]", parameters);
    }

    public async Task LogArtistPlay(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ArtistId", artistId);
        await _sql.Execute("[History].[LogArtistPlay]", parameters);
    }

    public async Task LogGenrePlay(string genre)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Genre", genre);
        await _sql.Execute("[History].[LogGenrePlay]", parameters);
    }

    public async Task LogGroupingPlay(int groupingId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@GroupingId", groupingId);
        await _sql.Execute("[History].[LogGroupingPlay]", parameters);
    }

    public async Task LogLibraryPlay()
    {
        await _sql.Execute("[History].[LogLibraryPlay]");
    }

    public async Task LogTagPlay(string tag)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Tag", tag);
        await _sql.Execute("[History].[LogTagPlay]", parameters);
    }

    public async Task LogTrackPlay(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TrackId", trackId);
        await _sql.Execute("[History].[LogTrackPlay]", parameters);
    }
}
