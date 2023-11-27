using Cadenza.API.Extensions;
using Cadenza.API.Interfaces.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly IAuthoriser _authoriser;
    private readonly IHistory _history;
    private readonly IAdminRepository _adminRepository;
    private readonly IUpdateRepository _updateRepository;

    public LastFmController(IAuthoriser authoriser, IHistory history, IAdminRepository adminRepository, IUpdateRepository updateRepository)
    {
        _authoriser = authoriser;
        _history = history;
        _adminRepository = adminRepository;
        _updateRepository = updateRepository;
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

    [HttpGet("TopAlbums")]
    public async Task<List<PlayedAlbumDTO>> TopAlbums(HistoryPeriod period, int limit, int page)
    {
        // TODO: Get from database
        return await _history.GetPlayedAlbums(period, limit, page);
    }

    [HttpGet("TopArtists")]
    public async Task<List<PlayedArtistDTO>> TopArtists(HistoryPeriod period, int limit, int page)
    {
        // TODO: Get from database
        return await _history.GetPlayedArtists(period, limit, page);
    }

    [HttpGet("TopTracks")]
    public async Task<List<PlayedTrackDTO>> TopTracks(HistoryPeriod period, int limit, int page)
    {
        // TODO: Get from database
        return await _history.GetPlayedTracks(period, limit, page);
    }
}
