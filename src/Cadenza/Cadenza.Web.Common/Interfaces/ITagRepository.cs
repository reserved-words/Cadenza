namespace Cadenza.Web.Common.Interfaces;

public interface ITagRepository
{
    Task<List<TaggedItemVM>> GetTag(string id);
}