using Cadenza.Library;
using Cadenza.API.Interfaces;
using Cadenza.API.Common.Interfaces;
using Cadenza.API.Common.Interfaces.Cache;
using Cadenza.API.Common.Model;

namespace Cadenza.API.Services;

public class UpdateService : IUpdateService
{
    private readonly IFileUpdateService _localFileUpdateQueue;
    private readonly ILibrary _library;
    private readonly IAlbumCache _albumCache;
    private readonly IArtistCache _artistCache;
    private readonly ITrackCache _trackCache;

    public UpdateService(IFileUpdateService updateQueue, ILibrary library, IArtistCache artistCache, IAlbumCache albumCache, ITrackCache trackCache)
    {
        _localFileUpdateQueue = updateQueue;
        _library = library;
        _artistCache = artistCache;
        _albumCache = albumCache;
        _trackCache = trackCache;
    }

    public async Task<FileUpdateQueue> GetUpdates()
    {
        return await _localFileUpdateQueue.Get();
    }

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var album = await _albumCache.GetAlbum(update.Id);

        if (album == null)
            return;

        await _albumCache.UpdateAlbum(update);
        await _library.UpdateAlbum(update);

        if (update.UpdatedItem.Source != LibrarySource.Local)
            return;

        foreach (var updatedItem in update.Updates)
        {
            await _localFileUpdateQueue.Add(updatedItem);
        }
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
            await _localFileUpdateQueue.Add(updatedItem);
        }
    }

    public async Task UpdateTrack(TrackUpdate update)
    {
        var track = await _trackCache.GetTrack(update.Id);

        if (track == null)
            return;

        await _trackCache.UpdateTrack(update);
        await _library.UpdateTrack(update);

        if (update.UpdatedItem.Source != LibrarySource.Local)
            return;

        foreach (var updatedItem in update.Updates)
        {
            await _localFileUpdateQueue.Add(updatedItem);
        }
    }
}
