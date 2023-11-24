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

    public async Task LogAlbumPlay(int albumId)
    {
        await _history.LogAlbumPlay(albumId);
    }

    public async Task LogArtistPlay(int artistId)
    {
        await _history.LogArtistPlay(artistId);
    }

    public async Task LogGenrePlay(string genre)
    {
        await _history.LogGenrePlay(genre);
    }

    public async Task LogGroupingPlay(int groupingId)
    {
        await _history.LogGroupingPlay(groupingId);
    }

    public async Task LogLibraryPlay()
    {
        await _history.LogLibraryPlay();
    }

    public async Task LogTagPlay(string tag)
    {
        await _history.LogTagPlay(tag);
    }

    public async Task LogTrackPlay(int trackId)
    {
        await _history.LogTrackPlay(trackId);
    }
}
