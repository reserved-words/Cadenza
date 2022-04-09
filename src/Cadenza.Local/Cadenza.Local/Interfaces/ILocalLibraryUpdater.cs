
namespace Cadenza.Local;

public interface ILocalLibraryUpdater
{
    Task UpdateModifiedFiles();
    Task UpdateDeletedFiles();
    Task RemovePlayedFiles();
    Task ProcessUpdateQueue();
}
