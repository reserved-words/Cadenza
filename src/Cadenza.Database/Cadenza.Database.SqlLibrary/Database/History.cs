﻿using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

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

    public async Task InsertScrobble(int trackId, DateTime scrobbledAt)
    {
        await _sql.Execute(new { TrackId = trackId, ScrobbledAt = scrobbledAt });
    }
}
