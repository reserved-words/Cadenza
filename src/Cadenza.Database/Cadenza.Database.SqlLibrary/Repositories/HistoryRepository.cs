using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class HistoryRepository : IHistoryRepository
{
    private readonly IHistory _history;
    private readonly IHistoryMapper _mapper;

    public HistoryRepository(IHistory history, IHistoryMapper mapper)
    {
        _history = history;
        _mapper = mapper;
    }

    public async Task<List<NewScrobbleDTO>> GetNewScrobbles()
    {
        var data = await _history.GetNewScrobbles();
        return data.Select(_mapper.MapScrobble).ToList();
    }

    public async Task<List<NowPlayingUpdateDTO>> GetNowPlayingUpdates()
    {
        var data = await _history.GetNowPlayingUpdates();
        return data.Select(_mapper.MapNowPlaying).ToList();
    }

    public async Task<List<RecentAlbumDTO>> GetRecentAlbums(int maxItems)
    {
        var data = await _history.GetRecentAlbums(maxItems);
        return data.Select(_mapper.MapRecentAlbum).ToList();
    }

    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        var data = await _history.GetRecentTags(maxItems);
        return data.Select(d => d.Tag).ToList();
    }

    public async Task<List<RecentTrackDTO>> GetRecentTracks(string username, int maxItems)
    {
        var data = await _history.GetRecentTracks(username, maxItems);
        return data.Select(_mapper.MapRecentTrack).ToList();
    }

    public async Task MarkNowPlayingFailed(int userId)
    {
        await _history.MarkNowPlayingFailed(userId);
    }

    public async Task MarkNowPlayingUpdated(int userId)
    {
        await _history.MarkNowPlayingUpdated(userId);
    }

    public async Task MarkScrobbled(int scrobbleId)
    {
        await _history.MarkScrobbled(scrobbleId);
    }

    public async Task MarkScrobbleFailed(int scrobbleId)
    {
        await _history.MarkScrobbleFailed(scrobbleId);
    }

    public async Task ScrobbleTrack(int trackId, string username, DateTime scrobbledAt)
    {
        await _history.InsertScrobble(new InsertScrobbleParameter
        {
            TrackId = trackId, 
            Username = username,
            ScrobbledAt = scrobbledAt
        });
    }

    public async Task SyncScrobbles()
    {
        await _history.SyncScrobbles();
    }

    public async Task UpdateNowPlaying(string username, int trackId, int secondsRemaining)
    {
        await _history.InsertNowPlaying(new InsertNowPlayingParameter
        {
            TrackId = trackId,
            Username = username,
            SecondsRemaining = secondsRemaining
        });
    }
}
