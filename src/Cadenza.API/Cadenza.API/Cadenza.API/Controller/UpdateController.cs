using Cadenza.API.Common.Controllers;
using Cadenza.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    private readonly IUpdateService _service;

    public UpdateController(IUpdateService service)
    {
        _service = service;
    }

    //[HttpGet("Get")]
    //public async Task<List<ItemUpdates>> Get()
    //{
    //    return await _service.GetQueuedUpdates();
    //}

    //[HttpPost("Remove")]
    //public async Task Update(ItemUpdates update)
    //{
    //    await _service.Update(update);
    //}

    [HttpPost("UpdateItem")]
    public async Task UpdateItem(ItemUpdates update)
    {
        await _service.Update(update);
    }
}