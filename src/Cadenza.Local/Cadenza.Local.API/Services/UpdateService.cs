using Cadenza.Library;
using Cadenza.Local.API.Interfaces;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Cache;
using Cadenza.Local.Common.Model;

namespace Cadenza.Local.API;

public class UpdateService : IUpdateService
{
    private readonly IFileUpdateService _id3UpdateQueue;
    private readonly ILibrary _library;
    private readonly IAlbumCache _albumCache;
    private readonly IArtistCache _artistCache;
    private readonly ITrackCache _trackCache;

    public UpdateService(IFileUpdateService updateQueue, ILibrary library, IArtistCache artistCache, IAlbumCache albumCache, ITrackCache trackCache)
    {
        _id3UpdateQueue = updateQueue;
        _library = library;
        _artistCache = artistCache;
        _albumCache = albumCache;
        _trackCache = trackCache;
    }

    public async Task<FileUpdateQueue> GetUpdates()
    {
        return await _id3UpdateQueue.Get();
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
            await _id3UpdateQueue.Add(updatedItem);
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
            await _id3UpdateQueue.Add(updatedItem);
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
            await _id3UpdateQueue.Add(updatedItem);
        }
    }
}
