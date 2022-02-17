using Cadenza.Domain;

namespace Cadenza.Local;

public interface IFileUpdateService
{
    Task Add(ItemPropertyUpdate update);
    Task<FileUpdateQueue> Get();
    Task LogError(ItemPropertyUpdate update, Exception ex);
    Task Remove(ItemPropertyUpdate update);
}
