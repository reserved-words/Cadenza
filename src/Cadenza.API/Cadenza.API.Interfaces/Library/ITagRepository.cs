namespace Cadenza.API.Interfaces.Library;

public interface ITagRepository
{
    Task<List<SearchItemDTO>> GetTag(string id);
}