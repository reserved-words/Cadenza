namespace Cadenza.API.Interfaces.Controllers;

public interface IAdminService
{
    Task<List<Grouping>> GetGroupings();
}
