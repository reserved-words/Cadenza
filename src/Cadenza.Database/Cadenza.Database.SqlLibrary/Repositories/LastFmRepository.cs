using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class LastFmRepository : ILastFmRepository
{
    private readonly ILastFm _lastFm;
    private readonly ILastFmMapper _mapper;

    public LastFmRepository(ILastFm lastFm, ILastFmMapper mapper)
    {
        _lastFm = lastFm;
        _mapper = mapper;
    }

    public async Task<List<LovedTrackUpdateDTO>> GetLovedTrackUpdates()
    {
        var updates = await _lastFm.GetLovedTrackUpdates();
        return updates.Select(_mapper.MapLovedTrack).ToList();
    }

    public async Task<List<NewScrobbleDTO>> GetNewScrobbles()
    {
        var data = await _lastFm.GetNewScrobbles();
        return data.Select(_mapper.MapScrobble).ToList();
    }

    public async Task<List<NowPlayingUpdateDTO>> GetNowPlayingUpdates()
    {
        var data = await _lastFm.GetNowPlayingUpdates();
        return data.Select(_mapper.MapNowPlaying).ToList();
    }

    public async Task MarkLovedTrackFailed(int userId, int trackId)
    {
        await _lastFm.MarkLovedTrackFailed(userId, trackId);
    }

    public async Task MarkLovedTrackUpdated(int userId, int trackId)
    {
        await _lastFm.MarkLovedTrackUpdated(userId, trackId);
    }

    public async Task MarkNowPlayingFailed(int userId)
    {
        await _lastFm.MarkNowPlayingFailed(userId);
    }

    public async Task MarkNowPlayingUpdated(int userId)
    {
        await _lastFm.MarkNowPlayingUpdated(userId);
    }

    public async Task MarkScrobbled(int scrobbleId)
    {
        await _lastFm.MarkScrobbled(scrobbleId);
    }

    public async Task MarkScrobbleFailed(int scrobbleId)
    {
        await _lastFm.MarkScrobbleFailed(scrobbleId);
    }
}
