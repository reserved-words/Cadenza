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

    public async Task MarkScrobbled(int scrobbleId)
    {
        await _history.MarkScrobbled(scrobbleId);
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
}
