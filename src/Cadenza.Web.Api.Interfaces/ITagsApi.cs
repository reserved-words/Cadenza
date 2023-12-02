namespace Cadenza.Web.Api.Interfaces;

public interface ITagsApi
{
    Task<List<TaggedItemVM>> GetTag(string id);
}