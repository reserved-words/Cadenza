
namespace Cadenza.Local;

public interface ILocalLibraryUpdater
{
    void UpdateAddedFiles();
    void UpdateModifiedFiles();
    void UpdateDeletedFiles();
    void RemovePlayedFiles();
    void ProcessUpdateQueue();
}
