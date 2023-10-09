namespace Cadenza.API.Interfaces.Library;

public interface ITagRepository
{
    Task<List<PlayerItemDTO>> GetTag(string id);
}