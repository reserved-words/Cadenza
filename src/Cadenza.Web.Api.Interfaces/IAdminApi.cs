namespace Cadenza.Web.Api.Interfaces;

public interface IAdminApi
{
    Task<List<GroupingVM>> GetGroupingOptions();
}
