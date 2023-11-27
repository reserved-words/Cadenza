using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Model;
using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class Scrobbler : IService
{
    private readonly ILastFmRepository _repository;
    private readonly ILastFmService _lastFmService;
    private readonly ILogger<Scrobbler> _logger;

    public Scrobbler(ILastFmRepository repository, ILastFmService lastFmService, ILogger<Scrobbler> logger)
    {
        _repository = repository;
        _lastFmService = lastFmService;
        _logger = logger;
    }

    public async Task Run()
    {
        var newScrobbles = await _repository.GetNewScrobbles();

        foreach (var newScrobble in newScrobbles)
        {
            await TryScrobble(newScrobble);
        }

        var nowPlayingUpdates = await _repository.GetNowPlayingUpdates();

        foreach (var nowPlayingUpdate in nowPlayingUpdates)
        {
            await TryUpdateNowPlaying(nowPlayingUpdate);
        }

        var lovedTrackUpdates = await _repository.GetLovedTrackUpdates();

        foreach (var lovedTrackUpdate in lovedTrackUpdates)
        {
            await TryUpdateLovedTrack(lovedTrackUpdate);
        }
    }

    private async Task TryUpdateLovedTrack(LovedTrackUpdateDTO update)
    {
        await _repository.MarkLovedTrackUpdated(update.UserId, update.TrackId);

        var track = new LovedTrack
        {
            Title = update.Track,
            Artist = update.Artist,
            IsLoved = update.IsLoved
        };

        await TryUpdateLovedTrack(update.UserId, update.TrackId, update.SessionKey, track);
    }

    private async Task TryUpdateLovedTrack(int userId, int trackId, string sessionKey, LovedTrack track)
    {
        try
        {
            await _lastFmService.UpdateLovedTrack(sessionKey, track);
        }
        catch (Exception ex)
        {
            await TryMarkLovedTrackFailed(userId, trackId);
            _logger.LogError(ex, "Failed to update loved track (User ID {0}, Track ID {1})", userId, trackId);
        }
    }

    private async Task TryUpdateNowPlaying(NowPlayingUpdateDTO nowPlayingUpdate)
    {
        await _repository.MarkNowPlayingUpdated(nowPlayingUpdate.UserId);

        // If update was so long ago that the seconds remaining have already passed don't bother updating
        if (nowPlayingUpdate.Timestamp.AddSeconds(nowPlayingUpdate.SecondsRemaining) < DateTime.Now)
            return;

        var nowPlaying = new NowPlaying
        {
            Duration = nowPlayingUpdate.SecondsRemaining,
            Title = nowPlayingUpdate.Track,
            Artist = nowPlayingUpdate.Artist,
            AlbumTitle = nowPlayingUpdate.Album,
            AlbumArtist = nowPlayingUpdate.AlbumArtist
        };

        await TryUpdateNowPlaying(nowPlayingUpdate.UserId, nowPlayingUpdate.SessionKey, nowPlaying);
    }

    private async Task TryUpdateNowPlaying(int userId, string sessionKey, NowPlaying nowPlaying)
    {
        try
        {
            await _lastFmService.UpdateNowPlaying(sessionKey, nowPlaying);
        }
        catch (Exception ex)
        {
            await TryMarkNowPlayingFailed(userId);
            _logger.LogError(ex, "Failed to update now playing (User ID {0})", userId);
        }
    }

    private async Task TryScrobble(NewScrobbleDTO newScrobble)
    {
        await _repository.MarkScrobbled(newScrobble.Id);

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

    private async Task TryMarkNowPlayingFailed(int userId)
    {
        try
        {
            await _repository.MarkNowPlayingFailed(userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to mark now playing attempt as failed (User ID {0})", userId);
        }
    }

    private async Task TryMarkScrobbleFailed(int scrobbleId)
    {
        try
        {
            await _repository.MarkScrobbleFailed(scrobbleId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to mark scrobble attempt as failed (ID {0})", scrobbleId);
        }
    }

    private async Task TryMarkLovedTrackFailed(int userId, int trackId)
    {
        try
        {
            await _repository.MarkLovedTrackFailed(userId, trackId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to mark loved track attempt as failed (User ID {0}, Track ID {1})", userId, trackId);
        }
    }
}