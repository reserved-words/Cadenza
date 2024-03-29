﻿using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.LastFm;

namespace Cadenza.Database.SqlLibrary.Database;

internal class LastFm : ILastFm
{
    private readonly ISqlAccess _sql;

    public LastFm(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(LastFm));
    }

    public async Task<List<GetLovedTrackUpdatesResult>> GetLovedTrackUpdates()
    {
        return await _sql.Query<GetLovedTrackUpdatesResult>(null);
    }

    public async Task<List<GetNewScrobblesResult>> GetNewScrobbles()
    {
        return await _sql.Query<GetNewScrobblesResult>(null);
    }

    public async Task<List<GetNowPlayingUpdatesResult>> GetNowPlayingUpdates()
    {
        return await _sql.Query<GetNowPlayingUpdatesResult>(null);
    }

    public async Task MarkLovedTrackFailed(int trackId)
    {
        await _sql.Execute(new { TrackId = trackId });
    }

    public async Task MarkLovedTrackUpdated(int trackId)
    {
        await _sql.Execute(new { TrackId = trackId });
    }

    public async Task MarkNowPlayingFailed()
    {
        await _sql.Execute(null);
    }

    public async Task MarkNowPlayingUpdated()
    {
        await _sql.Execute(null);
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
