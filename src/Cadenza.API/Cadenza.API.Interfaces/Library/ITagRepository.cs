using Cadenza.Common.Domain.Model;

namespace Cadenza.API.Interfaces.Library;

public interface ITagRepository
{
    Task<List<PlayerItem>> GetTag(string id);
}