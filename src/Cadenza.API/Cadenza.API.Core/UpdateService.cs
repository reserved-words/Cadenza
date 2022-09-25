using Cadenza.API.Common.Controllers;
using Cadenza.API.Common.Repositories;
using Cadenza.Domain.Models;

namespace Cadenza.API.Core;
internal class UpdateService : IUpdateService
{
    private readonly IUpdateRepository _repository;

    public UpdateService(IUpdateRepository repository)
    {
        _repository = repository;
    }

    public Task<List<ItemUpdates>> GetQueuedUpdates()
    {
        return Task.FromResult(new List<ItemUpdates>());
    }

    public async Task Update(ItemUpdates update)
    {
        await _repository.Add(update);


        // Apply this update to JSON library
        // Reorganise JSON library as appropriate

        // Repopulate cache
    }
}



//using Cadenza.API.Interfaces;
//using Cadenza.API.Common.Interfaces;
//using Cadenza.API.Common.Interfaces.Cache;
//using Cadenza.API.Common.Model;

//namespace Cadenza.API.Services;

//public class UpdateService : IUpdateService
//{
//    private readonly IUpdateRepository _fileUpdateService;
//    private readonly IMusicRepository _library;
//    private readonly IAlbumCache _albumCache;
//    private readonly IArtistCache _artistCache;
//    private readonly ITrackCache _trackCache;

//    public UpdateService(IUpdateRepository updateService, IMusicRepository library, IArtistCache artistCache, IAlbumCache albumCache, ITrackCache trackCache)
//    {
//        _fileUpdateService = updateService;
//        _library = library;
//        _artistCache = artistCache;
//        _albumCache = albumCache;
//        _trackCache = trackCache;
//    }

//    public async Task<FileUpdateQueue> GetAllUpdates()
//    {
//        return await _fileUpdateService.Get();
//    }

//    public async Task AddAlbumUpdate(AlbumUpdate update)
//    {
//        var album = await _albumCache.GetAlbum(update.Id);

//        if (album == null)
//            return;

//        await _albumCache.UpdateAlbum(update);
//        await _library.UpdateAlbum(update);

//        if (update.UpdatedItem.Source != LibrarySource.Local)
//            return;

//        foreach (var updatedItem in update.Updates)
//        {
//            await _fileUpdateService.Add(updatedItem);
//        }
//    }

//    public async Task AddArtistUpdate(ArtistUpdate update)
//    {
//        var artist = await _artistCache.GetArtist(update.Id);

//        if (artist == null)
//            return;

//        await _artistCache.UpdateArtist(update);
//        await _library.UpdateArtist(update);
//        foreach (var updatedItem in update.Updates)
//        {
//            await _fileUpdateService.Add(updatedItem);
//        }
//    }

//    public async Task AddTrackUpdate(TrackUpdate update)
//    {
//        var track = await _trackCache.GetTrack(update.Id);

//        if (track == null)
//            return;

//        await _trackCache.UpdateTrack(update);
//        await _library.UpdateTrack(update);

//        if (update.UpdatedItem.Source != LibrarySource.Local)
//            return;

//        foreach (var updatedItem in update.Updates)
//        {
//            await _fileUpdateService.Add(updatedItem);
//        }
//    }
//}

