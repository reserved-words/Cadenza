﻿namespace Cadenza.Web.Database.Repositories;

internal class PlayTrackRepository : IPlayTrackRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public PlayTrackRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<int>> PlayAll()
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayTracks);
    }

    public async Task<List<int>> PlayAlbum(int id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayAlbum, id);
    }

    public async Task<List<int>> PlayArtist(int id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayArtist, id);
    }

    public async Task<List<int>> PlayGenre(string id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayGenre, id);
    }

    public async Task<List<int>> PlayGrouping(int id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayGrouping, id);
    }

    public async Task<List<int>> PlayTag(string id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayTag, id);
    }
}
