using Cadenza.Common.Domain.Model;

namespace Cadenza.Web.Common.Interfaces.Library;

public interface ITagRepository
{
    Task<List<PlayerItemVM>> GetTag(string id);
}