namespace Cadenza.Local.Common.Interfaces;

public interface ILocalLibraryUpdater
{
    Task UpdateModifiedFiles();
    Task UpdateDeletedFiles();
    Task RemovePlayedFiles();
    Task ProcessUpdateQueue();
}
