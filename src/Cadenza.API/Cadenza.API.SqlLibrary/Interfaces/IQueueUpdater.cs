namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueUpdater
{
    Task QueueUpdates(ItemUpdates updates);
    Task MarkUpdatesDone(ItemUpdates updates);
}