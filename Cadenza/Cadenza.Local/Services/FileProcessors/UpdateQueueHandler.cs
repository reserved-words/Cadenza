namespace Cadenza.Local;

public class UpdateQueueHandler : IUpdateQueueHandler
{
    private readonly IFileUpdateService _service;
    private readonly IId3Updater _updater;

    public UpdateQueueHandler(IFileUpdateService service, IId3Updater udpater)
    {
        _service = service;
        _updater = udpater;
    }

    public void Process()
    {
        var queue = _service.Get();

        var updates = new Dictionary<ItemType, Dictionary<string, List<MetaDataUpdate>>>();

        var updatesToProcess = queue.Updates
            .Where(u => u.FailedAttempts.Count < 3)
            .ToList();

        foreach (var update in updatesToProcess)
        {
            if (!updates.ContainsKey(update.Update.ItemType))
            {
                updates.Add(update.Update.ItemType, new Dictionary<string, List<MetaDataUpdate>>());
            }

            if (!updates[update.Update.ItemType].ContainsKey(update.Update.Id))
            {
                updates[update.Update.ItemType].Add(update.Update.Id, new List<MetaDataUpdate>());
            }

            updates[update.Update.ItemType][update.Update.Id].Add(update.Update);
        }


        foreach (var itemType in updates.Keys)
        {
            foreach (var id in updates[itemType].Keys)
            {
                var itemUpdates = updates[itemType][id];

                ProcessUpdates(itemType, id, itemUpdates);
            }
        }
    }

    private void ProcessUpdates(ItemType itemType, string id, List<MetaDataUpdate> updates)
    {
        List<MetaDataUpdateResult> results;

        switch (itemType)
        {
            case ItemType.Artist:
                results = _updater.UpdateArtist(id, updates);
                break;
            case ItemType.Album:
                results = _updater.UpdateAlbum(id, updates);
                break;
            case ItemType.Track:
                results = _updater.UpdateTrack(id, updates);
                break;
            default:
                throw new NotImplementedException();
        }

        MarkProcessed(results);
    }

    private void MarkProcessed(List<MetaDataUpdateResult> results)
    {
        foreach (var result in results)
        {
            if (result.Completed)
            {
                _service.Remove(result.Update);
            }
            else if (result.Error != null)
            {
                _service.LogError(result.Update, result.Error);
            }
        }
    }
}
