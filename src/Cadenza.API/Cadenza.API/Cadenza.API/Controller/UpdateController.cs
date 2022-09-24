using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    //private readonly IUpdateService _service;

    //public UpdateController(IUpdateService service)
    //{
    //    _service = service;
    //}

    //[HttpGet("")]
    //public async Task<object> Get()
    //{
    //    return await _service.GetAllUpdates();
    //}

    //[HttpPost("")]
    //public async Task Album(AlbumUpdate update)
    //{
    //    await _service.AddAlbumUpdate(update);
    //}

    //[HttpPost("")]
    //public async Task Artist(ArtistUpdate update)
    //{
    //    await _service.AddArtistUpdate(update);
    //}

    //[HttpPost("")]
    //public async Task Track(TrackUpdate update)
    //{
    //    await _service.AddTrackUpdate(update);
    //}
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
