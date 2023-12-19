﻿using Cadenza.Database.SqlLibrary.Database.Interfaces;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Play : IPlay
{
    private readonly ISqlAccess _sql;

    public Play(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Play));
    }

    public async Task<List<int>> GetAlbumTrackIds(int id)
    {
        return await _sql.Query<int>(new { Id = id, LogRequest = true });
    }


    public async Task<List<int>> GetAllTrackIds()
    {
        return await _sql.Query<int>(new { LogRequest = true });
    }

    public async Task<List<int>> GetArtistTrackIds(int id)
    {
        return await _sql.Query<int>(new { Id = id, LogRequest = true });
    }

    public async Task<List<int>> GetGenreTrackIds(string genre, string grouping)
    {
        return await _sql.Query<int>(new { Genre = genre, Grouping = grouping, LogRequest = true });
    }

    public async Task<List<int>> GetGroupingTrackIds(string grouping)
    {
        return await _sql.Query<int>(new { Grouping = grouping, LogRequest = true });
    }

    public async Task<List<int>> GetTagTrackIds(string tag)
    {
        return await _sql.Query<int>(new { Tag = tag, LogRequest = true });
    }
}
