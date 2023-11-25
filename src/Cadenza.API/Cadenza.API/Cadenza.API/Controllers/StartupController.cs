namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StartupController : ControllerBase
{
    [HttpGet("Connect")]
    public IActionResult Connect()
    {
        return Ok();
    }
}
