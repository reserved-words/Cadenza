namespace Cadenza.API.Database.Services.Updaters;

internal class QueueUpdater : IQueueUpdater
{
    public void AddOrUpdate(List<EditedItem> queue, EditedItem updates)
    {
        var existingUpdate = queue.SingleOrDefault(e => e.Type == updates.Type && e.Id == updates.Id);

        if (existingUpdate == null)
        {
            queue.Add(updates);
        }
        else
        {
            foreach (var newPropertyUpdate in updates.Properties)
            {
                var existingPropertyUpdate = existingUpdate.Properties.SingleOrDefault(p => p.Property == newPropertyUpdate.Property);
                if (existingPropertyUpdate != null)
                {
                    existingUpdate.Properties.Remove(existingPropertyUpdate);
                }
                existingUpdate.Properties.Add(newPropertyUpdate);
            }
        }
    }

    public void Remove(List<EditedItem> updates, EditedItem update)
    {
        var queuedUpdate = updates.SingleOrDefault(u => u.Id == update.Id
            && u.Type == update.Type);

        if (queuedUpdate == null)
            return;

        var propertyUpdatesToRemove = new List<EditedProperty>();

        foreach (var queuedPropertyUpdate in queuedUpdate.Properties)
        {
            if (update.Properties.Any(u => u.Property == queuedPropertyUpdate.Property
                && u.UpdatedValue == queuedPropertyUpdate.UpdatedValue))
            {
                propertyUpdatesToRemove.Add(queuedPropertyUpdate);
            }
        }

        foreach (var propertyUpdate in propertyUpdatesToRemove)
        {
            queuedUpdate.Properties.Remove(propertyUpdate);
        }

        if (!queuedUpdate.Properties.Any())
        {
            updates.Remove(queuedUpdate);
        }
    }
}
