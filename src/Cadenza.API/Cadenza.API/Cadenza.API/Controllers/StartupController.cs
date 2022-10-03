﻿namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StartupController : ControllerBase
{
    private readonly IStartupService _service;

    public StartupController(IStartupService service)
    {
        _service = service;
    }

    [HttpGet("Connect")]
    public IActionResult Connect()
    {
        return Ok();
    }

    [HttpPost("Populate")]
    public async Task Populate()
    {
        await _service.Populate();
    }
}