using Cadenza.Domain;
using Cadenza.Local.Common.Model;

namespace Cadenza.Local.Common.Interfaces;

public interface IFileUpdateService
{
    Task Add(ItemPropertyUpdate update);
    Task<FileUpdateQueue> Get();
    Task LogError(ItemPropertyUpdate update, Exception ex);
    Task Remove(ItemPropertyUpdate update);
}
