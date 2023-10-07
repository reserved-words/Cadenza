namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _service;

    public AdminController(IAdminService service)
    {
        _service = service;
    }

    [HttpGet("GroupingOptions")]
    public async Task<List<GroupingDTO>> GroupingOptions()
    {
        return await _service.GetGroupings();
    }
}
