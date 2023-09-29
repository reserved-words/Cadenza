using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces;

public interface IAdminRepository
{
    Task<List<Grouping>> GetGroupingOptions();
}
