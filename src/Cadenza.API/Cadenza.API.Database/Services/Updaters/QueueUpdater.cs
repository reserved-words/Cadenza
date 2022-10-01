namespace Cadenza.API.Database.Services.Updaters;

internal class QueueUpdater : IQueueUpdater
{
    public void AddOrUpdate(List<ItemUpdates> queue, ItemUpdates updates)
    {
        var existingUpdate = queue.SingleOrDefault(e => e.Type == updates.Type && e.Id == updates.Id);

        if (existingUpdate == null)
        {
            queue.Add(updates);
        }
        else
        {
            foreach (var newPropertyUpdate in updates.Updates)
            {
                var existingPropertyUpdate = existingUpdate.Updates.SingleOrDefault(p => p.Property == newPropertyUpdate.Property);
                if (existingPropertyUpdate != null)
                {
                    existingUpdate.Updates.Remove(existingPropertyUpdate);
                }
                existingUpdate.Updates.Add(newPropertyUpdate);
            }
        }
    }

    public void Remove(List<ItemUpdates> updates, ItemUpdates update)
    {
        var queuedUpdate = updates.SingleOrDefault(u => u.Id == update.Id
            && u.Type == update.Type);

        if (queuedUpdate == null)
            return;

        var propertyUpdatesToRemove = new List<PropertyUpdate>();

        foreach (var queuedPropertyUpdate in queuedUpdate.Updates)
        {
            if (update.Updates.Any(u => u.Property == queuedPropertyUpdate.Property
                && u.UpdatedValue == queuedPropertyUpdate.UpdatedValue))
            {
                propertyUpdatesToRemove.Add(queuedPropertyUpdate);
            }
        }

        foreach (var propertyUpdate in propertyUpdatesToRemove)
        {
            queuedUpdate.Updates.Remove(propertyUpdate);
        }

        if (!queuedUpdate.Updates.Any())
        {
            updates.Remove(queuedUpdate);
        }
    }
}
