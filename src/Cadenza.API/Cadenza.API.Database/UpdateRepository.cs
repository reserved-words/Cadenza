using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Interfaces;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Updates;

namespace Cadenza.API.Database.Services;

internal class UpdateRepository : IUpdateRepository
{
    private readonly IDataAccess _dataAccess;

    public UpdateRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task Add(ItemUpdates update, LibrarySource? itemSource)
    {
        if (itemSource.HasValue)
        {
            await AddToSource(update, itemSource.Value);
        }
        else
        {
            foreach (var source in Enum.GetValues<LibrarySource>())
            {
                await AddToSource(update, source);
            }
        }
    }

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        return await _dataAccess.GetUpdates(source);
    }

    public async Task Remove(ItemUpdates update, LibrarySource itemSource)
    {
        var queuedUpdates = await _dataAccess.GetUpdates(itemSource);

        var queuedUpdate = queuedUpdates.SingleOrDefault(u => u.Id == update.Id
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
            queuedUpdates.Remove(queuedUpdate);
        }

        await _dataAccess.SaveUpdates(queuedUpdates, itemSource);

    }

    private async Task AddToSource(ItemUpdates update, LibrarySource source)
    {
        var updates = await _dataAccess.GetUpdates(source);

        AddOrUpdate(updates, update);

        await _dataAccess.SaveUpdates(updates, source);

    }

    private static void AddOrUpdate(List<ItemUpdates> queue, ItemUpdates newUpdate)
    {
        var existingUpdate = queue.SingleOrDefault(e => e.Type == newUpdate.Type && e.Id == newUpdate.Id);

        if (existingUpdate == null)
        {
            queue.Add(newUpdate);
        }
        else
        {
            foreach (var newPropertyUpdate in newUpdate.Updates)
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
}