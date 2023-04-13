namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueUpdater
{
    Task QueueUpdates(ItemUpdateRequest request, LibrarySource source);
    Task MarkUpdatesDone(ItemUpdateRequest request);
    Task MarkUpdatesErrored(ItemUpdateRequest request);
}