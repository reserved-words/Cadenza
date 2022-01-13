
namespace Cadenza.Local;

public interface IUpdater
{
    void UpdateAddedFiles();
    void UpdateModifiedFiles();
    void UpdateDeletedFiles();
    void RemovePlayedFiles();
    void ProcessUpdateQueue();
}
