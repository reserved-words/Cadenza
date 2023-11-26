using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Database;

internal class History : IHistory
{
    private readonly ISqlAccess _sql;

    public History(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(History));
    }

    public async Task<List<GetNewScrobblesResult>> GetNewScrobbles()
    {
        return await _sql.Query<GetNewScrobblesResult>(null);
    }

    public async Task<List<GetRecentAlbumsResult>> GetRecentAlbums(int maxItems)
    {
        return await _sql.Query<GetRecentAlbumsResult>(new { MaxItems = maxItems });
    }

    public async Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems)
    {
        return await _sql.Query<GetRecentTagsResult>(new { MaxItems = maxItems });
    }

    public async Task InsertScrobble(InsertScrobbleParameter parameters)
    {
        await _sql.Execute(parameters);
    }

    public async Task MarkScrobbled(int scrobbleId)
    {
        await _sql.Execute(new { ScrobbleId = scrobbleId });
    }

    public async Task MarkScrobbleFailed(int scrobbleId)
    {
        await _sql.Execute(new { ScrobbleId = scrobbleId });
    }
}
