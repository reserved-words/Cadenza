using Cadenza.Common.DTO;

namespace Cadenza.Database.Interfaces;

public interface IAdminRepository
{
    Task<List<GroupingDTO>> GetGroupings();
}
