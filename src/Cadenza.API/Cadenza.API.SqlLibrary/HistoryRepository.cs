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

    public async Task<List<RecentlyPlayedItem>> GetRecentlyPlayedItems(int maxItems)
    {
        var data = await _readService.GetRecentlyPlayedItems(maxItems);

        return data.Select(d => new RecentlyPlayedItem
        {
            TypeId = d.TypeId,
            ItemId = d.ItemId,
            PlaylistName = d.PlaylistName,
            ArtistName = d.ArtistName,
            AlbumTitle = d.AlbumTitle
        })
        .ToList();
    }

    public async Task LogAlbumPlay(int albumId)
    {
        await _insertService.LogAlbumPlay(albumId);
    }

    public async Task LogArtistPlay(string nameId)
    {
        await _insertService.LogArtistPlay(nameId);
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

    public async Task LogTrackPlay(string idFromSource)
    {
        await _insertService.LogTrackPlay(idFromSource);
    }
}
