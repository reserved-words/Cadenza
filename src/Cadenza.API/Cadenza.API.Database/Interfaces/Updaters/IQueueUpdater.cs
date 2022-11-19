namespace Cadenza.API.Database.Interfaces.Updaters;

internal interface IQueueUpdater
{
    void AddOrUpdate(List<EditedItem> queue, EditedItem updates);
    void Remove(List<EditedItem> queue, EditedItem update);
}