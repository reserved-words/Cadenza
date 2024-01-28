using Cadenza.Database.SqlLibrary.Database.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class AdminRepository : IAdminRepository
{
    private readonly IAdmin _admin;

    public AdminRepository(IAdmin admin)
    {
        _admin = admin;
    }

    public async Task<bool> HasLastFmSessionKey()
    {
        return await _admin.HasLastFmSessionKey();
    }

    public async Task SaveLastFmSessionKey(string lastFmUsername, string lastFmSessionKey)
    {
        await _admin.SaveLastFmSessionKey(lastFmUsername, lastFmSessionKey);
    }
}
