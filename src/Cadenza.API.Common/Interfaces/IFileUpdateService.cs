using Cadenza.API.Common.Model;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces;

public interface IFileUpdateService
{
    Task Add(ItemPropertyUpdate update);
    Task<FileUpdateQueue> Get();
    Task LogError(ItemPropertyUpdate update, Exception ex);
    Task Remove(ItemPropertyUpdate update);
}
