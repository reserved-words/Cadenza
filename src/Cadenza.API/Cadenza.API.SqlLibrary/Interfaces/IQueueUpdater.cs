namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueUpdater
{
    Task QueueUpdates(ItemUpdateRequestDTO request);
    Task MarkUpdateDone(ItemUpdateRequestDTO request);
    Task MarkUpdateErrored(ItemUpdateRequestDTO request);

    Task QueueRemoval(TrackRemovalRequestDTO request);
    Task MarkRemovalDone(int requestId);
    Task MarkRemovalErrored(int requestId);
}