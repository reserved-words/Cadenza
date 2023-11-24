using Cadenza.Database.SqlLibrary.Model.History;
using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class History : IHistory
{
    private readonly ISqlAccess _sql;

    public History(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(History));
    }

    public async Task<List<GetRecentAlbumsResult>> GetRecentAlbums(int maxItems)
    {
        return await _sql.Query<GetRecentAlbumsResult>(new { MaxItems = maxItems });
    }

    public async Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems)
    {
        return await _sql.Query<GetRecentTagsResult>(new { MaxItems = maxItems });
    }

    public async Task LogAlbumPlay(int albumId)
    {
        await _sql.Execute(new { AlbumId = albumId });
    }

    public async Task LogArtistPlay(int artistId)
    {
        await _sql.Execute(new { ArtistId = artistId });
    }

    public async Task LogGenrePlay(string genre)
    {
        await _sql.Execute(new { Genre = genre });
    }

    public async Task LogGroupingPlay(int groupingId)
    {
        await _sql.Execute(new { GroupingId = groupingId });
    }

    public async Task LogLibraryPlay()
    {
        await _sql.Execute(null);
    }

    public async Task LogTagPlay(string tag)
    {
        await _sql.Execute(new { Tag = tag});
    }

    public async Task LogTrackPlay(int trackId)
    {
        await _sql.Execute(new { TrackId = trackId });
    }
}
