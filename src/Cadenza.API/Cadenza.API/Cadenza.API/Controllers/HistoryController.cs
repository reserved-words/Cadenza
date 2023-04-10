namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IHistoryService _service;

    public HistoryController(IHistoryService service)
    {
        _service = service;
    }

    [HttpPost("LogAlbum/{albumId}")]
    public async Task LogAlbumPlay(int albumId)
    {
        await _service.LogAlbumPlay(albumId);
    }

    [HttpPost("LogArtist/{nameId}")]
    public async Task LogArtistPlay(string nameId)
    {
        await _service.LogArtistPlay(nameId);
    }

    [HttpPost("LogGenre/{genre}")]
    public async Task LogGenrePlay(string genre)
    {
        await _service.LogGenrePlay(genre);
    }

    [HttpPost("LogGrouping/{grouping}")]
    public async Task LogGroupingPlay(Grouping grouping)
    {
        await _service.LogGroupingPlay(grouping);
    }

    [HttpPost("LogLibrary")]
    public async Task LogLibraryPlay()
    {
        await _service.LogLibraryPlay();
    }

    [HttpPost("LogTag/{tag}")]
    public async Task LogTagPlay(string tag)
    {
        await _service.LogTagPlay(tag);
    }

    [HttpPost("LogTrack/{idFromSource}")]
    public async Task LogTrackPlay(string idFromSource)
    {
        await _service.LogTrackPlay(idFromSource);
    }
}
