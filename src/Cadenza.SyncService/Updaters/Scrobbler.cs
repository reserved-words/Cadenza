﻿using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Model;
using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class Scrobbler : IService
{
    private readonly IHistoryRepository _historyRepository;
    private readonly ILastFmService _lastFmService;
    private readonly ILogger<Scrobbler> _logger;

    public Scrobbler(IHistoryRepository repository, ILastFmService lastFmService, ILogger<Scrobbler> logger)
    {
        _historyRepository = repository;
        _lastFmService = lastFmService;
        _logger = logger;
    }

    public async Task Run()
    {
        var newScrobbles = await _historyRepository.GetNewScrobbles();

        foreach (var newScrobble in newScrobbles)
        {
            await TryScrobble(newScrobble);
        }
    }

    private async Task TryScrobble(NewScrobbleDTO newScrobble)
    {
        await _historyRepository.MarkScrobbled(newScrobble.Id);

        var scrobble = new Scrobble
        {
            Timestamp = newScrobble.ScrobbledAt,
            Title = newScrobble.Track,
            Artist = newScrobble.Artist,
            AlbumTitle = newScrobble.Album,
            AlbumArtist = newScrobble.AlbumArtist
        };

        await TryScrobble(newScrobble.Id, newScrobble.SessionKey, scrobble);
    }

    private async Task TryScrobble(int scrobbleId, string sessionKey, Scrobble scrobble)
    {
        try
        {
            await _lastFmService.ScrobbleTrack(sessionKey, scrobble);
        }
        catch (Exception ex)
        {
            await TryMarkScrobbleFailed(scrobbleId);
            _logger.LogError(ex, "Failed scrobble (ID {0})", scrobbleId);
        }
    }

    private async Task TryMarkScrobbleFailed(int scrobbleId)
    {
        try
        {
            await _historyRepository.MarkScrobbleFailed(scrobbleId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to mark scrobble attempt as failed (ID {0})", scrobbleId);
        }
    }
}