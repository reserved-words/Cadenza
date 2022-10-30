using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Repositories;

public interface ITagRepository
{
    Task<List<PlayerItem>> GetTag(string id);
}