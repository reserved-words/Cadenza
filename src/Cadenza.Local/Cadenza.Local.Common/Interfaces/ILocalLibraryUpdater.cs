namespace Cadenza.Local.Common.Interfaces;

public interface ILocalLibraryUpdater
{
    Task UpdateAddedFiles();
    Task UpdateModifiedFiles();
    Task UpdateDeletedFiles();
    Task RemovePlayedFiles();
    Task ProcessUpdateQueue();
}
