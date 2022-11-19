namespace Cadenza.API.Database.Interfaces.Updaters;

internal interface IQueueUpdater
{
    void Queue(List<EditedItem> queue, EditedItem update);
    void Dequeue(List<EditedItem> queue, EditedItem update);
}