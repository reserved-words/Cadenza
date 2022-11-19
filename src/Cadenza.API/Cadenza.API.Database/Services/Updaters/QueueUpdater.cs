namespace Cadenza.API.Database.Services.Updaters;

internal class QueueUpdater : IQueueUpdater
{
    public void Queue(List<EditedItem> queue, EditedItem update)
    {
		var queuedUpdate = GetQueuedItemUpdate(queue, update);

		if (queuedUpdate == null)
		{
			AddItemToQueue(queue, update);
		}
		else
		{
			UpdateQueuedItem(update, queuedUpdate);
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

	private static void AddItemToQueue(List<EditedItem> queue, EditedItem update)
	{
		queue.Add(update);
	}

	private static EditedItem GetQueuedItemUpdate(List<EditedItem> queue, EditedItem newItemUpdate)
	{
		return queue.SingleOrDefault(e => e.Type == newItemUpdate.Type && e.Id == newItemUpdate.Id);
	}

	private static EditedProperty GetQueuedPropertyUpdate(EditedItem queuedItem, EditedProperty newPropertyUpdate)
	{
		return queuedItem.Properties.SingleOrDefault(p => p.Property == newPropertyUpdate.Property);
	}

	private static void UpdateQueuedItem(EditedItem update, EditedItem queuedUpdate)
	{
		foreach (var newPropertyUpdate in update.Properties)
		{
			var existingPropertyUpdate = GetQueuedPropertyUpdate(queuedUpdate, newPropertyUpdate);
			if (existingPropertyUpdate != null)
			{
				existingPropertyUpdate.UpdatedValue = newPropertyUpdate.UpdatedValue;
			}
			else
			{
				queuedUpdate.Properties.Add(newPropertyUpdate);
			}
		}
	}
}
