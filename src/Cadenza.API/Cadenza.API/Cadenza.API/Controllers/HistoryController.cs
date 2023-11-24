namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IHistoryRepository _repository;

    public HistoryController(IHistoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("RecentAlbumRequests/{maxItems}")]
    public async Task<List<RecentAlbumDTO>> RecentAlbumRequests(int maxItems)
    {
        return await _repository.GetRecentAlbums(maxItems);
    }

    [HttpGet("RecentTagRequests/{maxItems}")]
    public async Task<List<string>> RecentTagRequests(int maxItems)
    {
        return await _repository.GetRecentTags(maxItems);
    }
}
