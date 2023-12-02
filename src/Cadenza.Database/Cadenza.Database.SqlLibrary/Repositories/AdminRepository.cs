using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class AdminRepository : IAdminRepository
{
    private readonly IAdmin _admin;
    private readonly IAdminMapper _mapper;

    public AdminRepository(IAdmin admin, IAdminMapper mapper)
    {
        _admin = admin;
        _mapper = mapper;
    }

    public async Task<List<GroupingDTO>> GetGroupings()
    {
        var groupings = await _admin.GetGroupings();
        return groupings.Select(_mapper.MapGrouping).ToList();
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
