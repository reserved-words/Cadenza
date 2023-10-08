namespace Cadenza.Web.Common.Interfaces;

public interface ITagRepository
{
    Task<List<PlayerItemVM>> GetTag(string id);
}