using Cadenza.Common.Domain.Model;
using Cadenza.Web.Model;

namespace Cadenza.Web.Common.Interfaces.Library;

public interface ITagRepository
{
    Task<List<PlayerItemVM>> GetTag(string id);
}