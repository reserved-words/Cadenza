using Cadenza.Domain;

namespace Cadenza.Local;

public interface IFileUpdateService
{
    void Add(ItemPropertyUpdate update);
    FileUpdateQueue Get();
    void LogError(ItemPropertyUpdate update, Exception ex);
    void Remove(ItemPropertyUpdate update);
}
