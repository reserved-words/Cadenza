using Cadenza.API.Interfaces.LastFm;
using Cadenza.Common.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly ILastFmInfoService _infoService;
    private readonly ILastFmSessionService _sessionService;
    private readonly IAdminRepository _repository;

    public LastFmController(ILastFmSessionService authoriser, IAdminRepository adminRepository, ILastFmInfoService infoService)
    {
        _sessionService = authoriser;
        _repository = adminRepository;
        _infoService = infoService;
    }


    [HttpGet("AuthUrl")]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _sessionService.GetAuthUrl(redirectUri);
    }

    [HttpPost("CreateSession")]
    public async Task CreateSession(string token)
    {
        var session = await _sessionService.CreateSession(token);
        var username = HttpContext.GetUsername();
        await _repository.SaveLastFmSessionKey(username, session.Username, session.SessionKey);
    }

    [HttpGet("HasSession")]
    public async Task<bool> HasSession()
    {
        var username = HttpContext.GetUsername();
        return await _repository.HasLastFmSessionKey(username);
    }

    [HttpGet("AlbumArtworkUrl")]
    public async Task<AlbumArtworkDTO> AlbumArtworkUrl(string artist, string title)
    {
        var url = await _infoService.AlbumArtworkUrl(artist, title);
        return new AlbumArtworkDTO { Url = url };
    }
}
