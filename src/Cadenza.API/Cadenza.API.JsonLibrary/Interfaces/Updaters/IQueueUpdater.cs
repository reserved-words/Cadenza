namespace Cadenza.API.JsonLibrary.Interfaces.Updaters;

internal interface IQueueUpdater
{
    void AddOrUpdate(List<ItemUpdates> queue, ItemUpdates updates);
    void Remove(List<ItemUpdates> queue, ItemUpdates update);
}