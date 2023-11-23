namespace Cadenza.Database.SqlLibrary.Repositories;

internal class AdminRepository : IAdminRepository
{
    private readonly IAdmin _admin;

    public AdminRepository(IAdmin admin)
    {
        _admin = admin;
    }

    public async Task<List<GroupingDTO>> GetGroupings()
    {
        return await _admin.GetGroupings();
    }
}
