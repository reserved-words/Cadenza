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
		var queuedUpdate = GetQueuedItemUpdate(queue, update);

		if (queuedUpdate == null)
			return;

		var propertiesToRemove = GetMatchingProperties(queuedUpdate, update);

		foreach (var property in propertiesToRemove)
		{
			queuedUpdate.Properties.Remove(property);
		}

		if (!queuedUpdate.Properties.Any())
		{
			queue.Remove(queuedUpdate);
		}
	}

	private static List<EditedProperty> GetMatchingProperties(EditedItem queuedUpdate, EditedItem compareUpdate)
	{
		return queuedUpdate.Properties
			.Where(p => compareUpdate.Properties.Any(u => u.Property == p.Property && u.UpdatedValue == p.UpdatedValue))
			.ToList();
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
		foreach (var newUpdate in update.Properties)
		{
			var existingUpdate = GetQueuedPropertyUpdate(queuedUpdate, newUpdate);

			if (existingUpdate != null)
			{
				existingUpdate.UpdatedValue = newUpdate.UpdatedValue;
			}
			else
			{
				queuedUpdate.Properties.Add(newUpdate);
			}
		}
	}
}
