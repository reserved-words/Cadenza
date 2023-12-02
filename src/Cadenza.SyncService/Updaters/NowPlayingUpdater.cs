using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Model;
using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class NowPlayingUpdater : IService
{
    private readonly ILastFmRepository _repository;
    private readonly ILastFmUserService _lastFmService;
    private readonly ILogger<NowPlayingUpdater> _logger;

    public NowPlayingUpdater(ILastFmRepository repository, ILastFmUserService lastFmService, ILogger<NowPlayingUpdater> logger)
    {
        _repository = repository;
        _lastFmService = lastFmService;
        _logger = logger;
    }

    public async Task Run()
    {
        var nowPlayingUpdates = await _repository.GetNowPlayingUpdates();

        foreach (var nowPlayingUpdate in nowPlayingUpdates)
        {
            await TryUpdateNowPlaying(nowPlayingUpdate);
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
}
