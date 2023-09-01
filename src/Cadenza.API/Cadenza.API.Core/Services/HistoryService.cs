namespace Cadenza.API.Core.Services;

internal class HistoryService : IHistoryService
{
    private readonly IHistoryRepository _repository;

    public HistoryService(IHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RecentAlbum>> GetRecentAlbums(int maxItems)
    {
        return await _repository.GetRecentAlbums(maxItems);
    }


    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        return await _repository.GetRecentTags(maxItems);
    }
    public async Task LogAlbumPlay(int albumId)
    {
        await _repository.LogAlbumPlay(albumId);
    }

    public async Task LogArtistPlay(int artistId)
    {
        await _repository.LogArtistPlay(artistId);
    }

    public async Task LogGenrePlay(string genre)
    {
        await _repository.LogGenrePlay(genre);
    }

    public async Task LogGroupingPlay(Grouping grouping)
    {
        await _repository.LogGroupingPlay(grouping);
    }

    public async Task LogLibraryPlay()
    {
        await _repository.LogLibraryPlay();
    }

    public async Task LogTagPlay(string tag)
    {
        await _repository.LogTagPlay(tag);
    }

    public async Task LogTrackPlay(int trackId)
    {
        await _repository.LogTrackPlay(trackId);
    }
}
