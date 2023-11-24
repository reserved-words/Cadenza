using Cadenza.API.Interfaces.Services;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StartupController : ControllerBase
{
    private readonly ICachePopulater _populater;

    public StartupController(ICachePopulater populater)
    {
        _populater = populater;
    }

    [HttpGet("Connect")]
    public IActionResult Connect()
    {
        return Ok();
    }

    [HttpPost("Populate")]
    public async Task Populate()
    {
        await _populater.Populate(false);
    }
}
