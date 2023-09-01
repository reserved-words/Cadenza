﻿using Cadenza.API.Interfaces;

namespace Cadenza.API.Core.Services;

internal class PlayTrackService : IPlayTrackService
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public PlayTrackService(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    public async Task<List<PlayTrack>> GetPlayTracks()
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayAll();
    }

    public async Task<List<PlayTrack>> GetPlayTracksByAlbum(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayAlbum(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByArtist(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayArtist(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByGenre(string id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayGenre(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByGrouping(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayGrouping(id);
    }

    public async Task<List<PlayTrack>> GetPlayTracksByTag(string id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayTag(id);
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
