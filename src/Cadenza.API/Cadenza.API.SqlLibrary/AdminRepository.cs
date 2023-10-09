namespace Cadenza.API.SqlLibrary;

internal class AdminRepository : IAdminRepository
{
    private readonly IDataReadService _readService;

    public AdminRepository(IDataReadService readService)
    {
        _readService = readService;
    }

    public async Task<List<GroupingDTO>> GetGroupings()
    {
        return await _readService.GetGroupings();
    }
}
