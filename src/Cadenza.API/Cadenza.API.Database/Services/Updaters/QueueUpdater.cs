namespace Cadenza.API.Database.Services.Updaters;

internal class QueueUpdater : IQueueUpdater
{
    public void Queue(List<EditedItem> queue, EditedItem update)
    {
        var queuedUpdate = queue.SingleOrDefault(e => e.Type == update.Type && e.Id == update.Id);

        if (queuedUpdate == null)
        {
            queue.Add(update);
        }
        else
        {
            foreach (var newPropertyUpdate in update.Properties)
            {
                var existingPropertyUpdate = queuedUpdate.Properties.SingleOrDefault(p => p.Property == newPropertyUpdate.Property);
                if (existingPropertyUpdate != null)
                {
                    queuedUpdate.Properties.Remove(existingPropertyUpdate);
                }
                queuedUpdate.Properties.Add(newPropertyUpdate);
            }
        }
    }

    public void Dequeue(List<EditedItem> queue, EditedItem update)
    {
        var queuedUpdate = queue.SingleOrDefault(u => u.Id == update.Id && u.Type == update.Type);

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
            queue.Remove(queuedUpdate);
        }
	}
}
