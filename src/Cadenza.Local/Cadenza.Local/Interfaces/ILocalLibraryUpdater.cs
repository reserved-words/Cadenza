
namespace Cadenza.Local;

public interface ILocalLibraryUpdater
{
    Task UpdateAddedFiles();
    Task UpdateModifiedFiles();
    Task UpdateDeletedFiles();
    Task RemovePlayedFiles();
    Task ProcessUpdateQueue();
}
