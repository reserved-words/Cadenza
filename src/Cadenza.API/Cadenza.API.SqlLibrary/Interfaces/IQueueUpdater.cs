namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueUpdater
{
    Task QueueUpdates(ItemUpdates updates, LibrarySource source);
    Task MarkUpdatesDone(ItemUpdates updates);
    Task MarkUpdatesErrored(ItemUpdates updates);
}