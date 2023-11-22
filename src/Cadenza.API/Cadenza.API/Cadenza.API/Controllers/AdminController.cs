namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _repository;

    public AdminController(IAdminRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GroupingOptions")]
    public async Task<List<GroupingDTO>> GroupingOptions()
    {
        return await _repository.GetGroupings();
    }
}
