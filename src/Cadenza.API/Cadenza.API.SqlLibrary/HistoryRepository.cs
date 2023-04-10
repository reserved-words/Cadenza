using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary;

internal class HistoryRepository : IHistoryRepository
{
    private readonly IDataInsertService _service;

    public HistoryRepository(IDataInsertService service)
    {
        _service = service;
    }

    public async Task LogAlbumPlay(int albumId)
    {
        await _service.LogAlbumPlay(albumId);
    }

    public async Task LogArtistPlay(string nameId)
    {
        await _service.LogArtistPlay(nameId);
    }

    public async Task LogGenrePlay(string genre)
    {
        await _service.LogGenrePlay(genre);
    }

    public async Task LogGroupingPlay(Grouping grouping)
    {
        await _service.LogGroupingPlay((int)grouping);
    }

    public async Task LogLibraryPlay()
    {
        await _service.LogLibraryPlay();
    }

    public async Task LogTagPlay(string tag)
    {
        await _service.LogTagPlay(tag);
    }

    public async Task LogTrackPlay(string idFromSource)
    {
        await _service.LogTrackPlay(idFromSource);
    }
}
