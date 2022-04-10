using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.FileProcessors;

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

    public async Task Process()
    {
        var queue = await _service.Get();

        var updates = new Dictionary<LibraryItemType, Dictionary<string, List<ItemPropertyUpdate>>>();

        var updatesToProcess = queue.Updates
            .Where(u => u.FailedAttempts.Count < 3)
            .ToList();

        foreach (var update in updatesToProcess)
        {
            if (!updates.ContainsKey(update.Update.ItemType))
            {
                updates.Add(update.Update.ItemType, new Dictionary<string, List<ItemPropertyUpdate>>());
            }

            if (!updates[update.Update.ItemType].ContainsKey(update.Update.Id))
            {
                updates[update.Update.ItemType].Add(update.Update.Id, new List<ItemPropertyUpdate>());
            }

            updates[update.Update.ItemType][update.Update.Id].Add(update.Update);
        }


        foreach (var itemType in updates.Keys)
        {
            foreach (var id in updates[itemType].Keys)
            {
                var itemUpdates = updates[itemType][id];

                await ProcessUpdates(itemType, id, itemUpdates);
            }
        }
    }

    private async Task ProcessUpdates(LibraryItemType itemType, string id, List<ItemPropertyUpdate> updates)
    {
        List<ItemPropertyUpdateResult> results;

        switch (itemType)
        {
            case LibraryItemType.Artist:
                results = await _updater.UpdateArtist(id, updates);
                break;
            case LibraryItemType.Album:
                results = await _updater.UpdateAlbum(id, updates);
                break;
            case LibraryItemType.Track:
                results = await _updater.UpdateTrack(id, updates);
                break;
            default:
                throw new NotImplementedException();
        }

        MarkProcessed(results);
    }

    private void MarkProcessed(List<ItemPropertyUpdateResult> results)
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
