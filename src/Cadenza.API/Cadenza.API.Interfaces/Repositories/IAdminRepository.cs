namespace Cadenza.API.Interfaces.Repositories;

public interface IAdminRepository
{
    Task<List<Grouping>> GetGroupings();
}
