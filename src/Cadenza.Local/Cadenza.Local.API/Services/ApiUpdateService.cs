using Cadenza.Library;
using Cadenza.Local.API.Interfaces;

namespace Cadenza.Local.API;

public class ApiUpdateService : IApiUpdateService
{
    private readonly IFileUpdateService _id3UpdateQueue;
    private readonly ILibrary _library;
    private readonly IArtistCache _artistCache;

    public ApiUpdateService(IFileUpdateService updateQueue, ILibrary library, IArtistCache artistCache)
    {
        _id3UpdateQueue = updateQueue;
        _library = library;
        _artistCache = artistCache;
    }

    public async Task<FileUpdateQueue> GetUpdates()
    {
        return await _id3UpdateQueue.Get();
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var artist = await _artistCache.GetArtist(update.Id);

        if (artist == null)
            return;

        await _artistCache.UpdateArtist(update);
        await _library.UpdateArtist(update);
        foreach (var updatedItem in update.Updates)
        {
            await _id3UpdateQueue.Add(updatedItem);
        }
    }
}
