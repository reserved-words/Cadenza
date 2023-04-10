﻿using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Database.Repositories;

internal class HistoryLogger : IHistoryLogger
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHelper _apiHelper;

    public HistoryLogger(IOptions<DatabaseApiSettings> settings, IApiHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task LogPlayedItem(PlaylistId playlistId)
    {
        switch (playlistId.Type)
        {
            case PlaylistType.All:
                await _apiHelper.Post(_settings.LogLibraryPlay);
                break;
            case PlaylistType.Artist:
                await _apiHelper.Post($"{_settings.LogArtistPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Album:
                await _apiHelper.Post($"{_settings.LogAlbumPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Track:
                await _apiHelper.Post($"{_settings.LogTrackPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Genre:
                await _apiHelper.Post($"{_settings.LogGenrePlay}/{playlistId.Id}");
                break;
            case PlaylistType.Grouping:
                await _apiHelper.Post($"{_settings.LogGroupingPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Tag:
                await _apiHelper.Post($"{_settings.LogTagPlay}/{playlistId.Id}");
                break;
            default:
                throw new NotImplementedException($"No method implemented to log played item of type {playlistId.Type}");
        }
    }

}
