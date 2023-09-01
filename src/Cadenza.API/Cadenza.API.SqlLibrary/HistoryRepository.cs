using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary;

internal class HistoryRepository : IHistoryRepository
{
    private readonly IDataInsertService _insertService;
    private readonly IDataReadService _readService;

    public HistoryRepository(IDataInsertService service, IDataReadService readService)
    {
        _insertService = service;
        _readService = readService;
    }

    public async Task<List<RecentAlbum>> GetRecentAlbums(int maxItems)
    {
        var data = await _readService.GetRecentAlbums(maxItems);

        return data.Select(d => new RecentAlbum
        {
            Id = d.AlbumId,
            Title = d.AlbumTitle,
            ArtistName = d.ArtistName
        })
        .ToList();
    }

    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        var data = await _readService.GetRecentTags(maxItems);
        return data.Select(d => d.Tag).ToList();
    }

    public async Task LogAlbumPlay(int albumId)
    {
        await _insertService.LogAlbumPlay(albumId);
    }

    public async Task LogArtistPlay(int artistId)
    {
        await _insertService.LogArtistPlay(artistId);
    }

    public async Task LogGenrePlay(string genre)
    {
        await _insertService.LogGenrePlay(genre);
    }

    public async Task LogGroupingPlay(Grouping grouping)
    {
        await _insertService.LogGroupingPlay((int)grouping);
    }

    public async Task LogLibraryPlay()
    {
        await _insertService.LogLibraryPlay();
    }

    public async Task LogTagPlay(string tag)
    {
        await _insertService.LogTagPlay(tag);
    }

    public async Task LogTrackPlay(int trackId)
    {
        await _insertService.LogTrackPlay(trackId);
    }
}
