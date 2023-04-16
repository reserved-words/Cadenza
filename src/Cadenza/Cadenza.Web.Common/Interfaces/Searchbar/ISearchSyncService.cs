namespace Cadenza.Web.Common.Interfaces.Searchbar;

public interface ISearchSyncService
{
    Task<List<PlayerItem>> GetSearchItems();
}
