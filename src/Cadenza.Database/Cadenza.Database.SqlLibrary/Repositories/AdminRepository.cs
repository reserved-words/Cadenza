using Cadenza.Database.SqlLibrary.Database.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class AdminRepository : IAdminRepository
{
    private readonly IAdmin _admin;

    public AdminRepository(IAdmin admin)
    {
        _admin = admin;
    }

    public async Task<bool> HasLastFmSessionKey(string username)
    {
        return await _admin.HasLastFmSessionKey(username);
    }

    public async Task SaveLastFmSessionKey(string username, string lastFmUsername, string lastFmSessionKey)
    {
        await _admin.SaveLastFmSessionKey(username, lastFmUsername, lastFmSessionKey);
    }
}
