namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueUpdater
{
    Task QueueUpdates(ItemUpdateRequest request);
    Task MarkUpdateDone(ItemUpdateRequest request);
    Task MarkUpdateErrored(ItemUpdateRequest request);

    Task QueueRemoval(TrackRemovalRequest request);
    Task MarkRemovalDone(int requestId);
    Task MarkRemovalErrored(int requestId);
}