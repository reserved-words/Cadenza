﻿using Cadenza.API.JsonLibrary.Interfaces;
using Cadenza.API.JsonLibrary.Interfaces.Updaters;

namespace Cadenza.API.JsonLibrary;

internal class UpdateRepository : IUpdateRepository
{
    private readonly IDataAccess _dataAccess;
    private readonly IQueueUpdater _updater;

    public UpdateRepository(IDataAccess dataAccess, IQueueUpdater updater)
    {
        _dataAccess = dataAccess;
        _updater = updater;
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

    public async Task MarkAsDone(ItemUpdates update, LibrarySource source)
    {
        await _dataAccess.UpdateUpdates(source, updates =>
        {
            _updater.Remove(updates, update);
        });
    }

    public Task MarkAsErrored(ItemUpdates update, LibrarySource source)
    {
        // Not implemented in this library type
        return Task.CompletedTask;
    }

    private async Task AddToSource(ItemUpdates update, LibrarySource source)
    {
        await _dataAccess.UpdateUpdates(source, updates =>
        {
            _updater.AddOrUpdate(updates, update);
        });
    }
}