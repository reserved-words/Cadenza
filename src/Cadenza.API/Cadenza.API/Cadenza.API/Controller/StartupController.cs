using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class StartupController : ControllerBase
{
    [HttpGet]
    public IActionResult Connect()
    {
        return Ok();
    }
}
