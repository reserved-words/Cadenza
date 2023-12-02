using Cadenza.API.Extensions;
using Cadenza.API.Interfaces.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly ILastFmSessionService _authoriser;
    private readonly IAdminRepository _adminRepository;

    public LastFmController(ILastFmSessionService authoriser, IAdminRepository adminRepository)
    {
        _authoriser = authoriser;
        _adminRepository = adminRepository;
    }


    [HttpGet("AuthUrl")]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    [HttpPost("CreateSession")]
    public async Task CreateSession(string token)
    {
        var session = await _authoriser.CreateSession(token);
        var username = HttpContext.GetUsername();
        await _adminRepository.SaveLastFmSessionKey(username, session.Username, session.SessionKey);
    }

    [HttpGet("HasSession")]
    public async Task<bool> HasSession()
    {
        var username = HttpContext.GetUsername();
        return await _adminRepository.HasLastFmSessionKey(username);
    }
}
