using Cadenza.API.Extensions;
using Cadenza.API.Interfaces.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly IAuthoriser _authoriser;
    private readonly IAdminRepository _adminRepository;
    private readonly IHistoryRepository _historyRepository;

    public LastFmController(IAuthoriser authoriser, IAdminRepository adminRepository, IHistoryRepository historyRepository)
    {
        _authoriser = authoriser;
        _adminRepository = adminRepository;
        _historyRepository = historyRepository;
    }


    [HttpGet("AuthUrl")]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    // TODO: Should be POST
    [HttpGet("CreateSession")]
    public async Task<string> CreateSession(string token)
    {
        var session = await _authoriser.CreateSession(token);
        var username = HttpContext.GetUsername();
        await _adminRepository.SaveLastFmSessionKey(username, session.Username, session.SessionKey);
        return session.SessionKey;
    }

    // TODO: Move to HistoryController
    [HttpGet("TopAlbums")]
    public async Task<List<TopAlbumDTO>> TopAlbums(HistoryPeriod period, int maxItems)
    {
        // TODO: Get from database
        return await _historyRepository.GetTopAlbums(period, maxItems);
    }

    // TODO: Move to HistoryController
    [HttpGet("TopArtists")]
    public async Task<List<TopArtistDTO>> TopArtists(HistoryPeriod period, int maxItems)
    {
        // TODO: Get from database
        return await _historyRepository.GetTopArtists(period, maxItems);
    }

    // TODO: Move to HistoryController
    [HttpGet("TopTracks")]
    public async Task<List<TopTrackDTO>> TopTracks(HistoryPeriod period, int maxItems)
    {
        // TODO: Get from database
        return await _historyRepository.GetTopTracks(period, maxItems);
    }
}
