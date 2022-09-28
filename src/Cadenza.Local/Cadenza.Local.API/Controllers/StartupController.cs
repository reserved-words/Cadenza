namespace Cadenza.Local.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StartupController : ControllerBase
{
    [HttpGet("Connect")]
    public IActionResult Connect()
    {
        return new OkResult();
    }
}
