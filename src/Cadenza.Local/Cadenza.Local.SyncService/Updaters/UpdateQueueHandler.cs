using Cadenza.Domain;
using Cadenza.Local.Common.Interfaces;

namespace Cadenza.Local.SyncService.Updaters;

public class UpdateQueueHandler : IUpdateService
{
    private readonly IFileUpdateService _service;
    private readonly ILocalFilesUpdater _updater;

    public UpdateQueueHandler(IFileUpdateService service, ILocalFilesUpdater udpater)
    {
        _service = service;
        _updater = udpater;
    }

    public async Task Run()
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
        try
        {
            switch (itemType)
            {
                case LibraryItemType.Artist:
                    await _updater.UpdateArtist(id, updates);
                    break;
                case LibraryItemType.Album:
                    await _updater.UpdateAlbum(id, updates);
                    break;
                case LibraryItemType.Track:
                    await _updater.UpdateTrack(id, updates);
                    break;
                default:
                    throw new NotImplementedException();
            }

            MarkProcessed(updates, null);
        }
        catch (Exception ex)
        {
            MarkProcessed(updates, ex);
        }
    }

    private void MarkProcessed(List<ItemPropertyUpdate> updates, Exception error)
    {
        foreach (var update in updates)
        {
            if (error == null)
            {
                _service.Remove(update);
            }
            else
            {
                _service.LogError(update, error);
            }
        }
    }
}
