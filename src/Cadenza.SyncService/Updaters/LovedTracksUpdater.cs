using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Model;
using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class LovedTracksUpdater : IService
{
    private readonly ILastFmRepository _repository;
    private readonly ILastFmUserService _lastFmService;
    private readonly ILogger<LovedTracksUpdater> _logger;

    public LovedTracksUpdater(ILastFmRepository repository, ILastFmUserService lastFmService, ILogger<LovedTracksUpdater> logger)
    {
        _repository = repository;
        _lastFmService = lastFmService;
        _logger = logger;
    }

    public async Task Run()
    {
        var lovedTrackUpdates = await _repository.GetLovedTrackUpdates();

        foreach (var lovedTrackUpdate in lovedTrackUpdates)
        {
            await TryUpdateLovedTrack(lovedTrackUpdate);
        }
    }

    private async Task TryUpdateLovedTrack(LovedTrackUpdateDTO update)
    {
        await _repository.MarkLovedTrackUpdated(update.TrackId);

        var track = new LovedTrack
        {
            Title = update.Track,
            Artist = update.Artist,
            IsLoved = update.IsLoved
        };

        await TryUpdateLovedTrack(update.TrackId, update.SessionKey, track);
    }

    private async Task TryUpdateLovedTrack(int trackId, string sessionKey, LovedTrack track)
    {
        try
        {
            await _lastFmService.UpdateLovedTrack(sessionKey, track);
        }
        catch (Exception ex)
        {
            await TryMarkLovedTrackFailed(trackId);
            _logger.LogError(ex, "Failed to update loved track (Track ID {1})", trackId);
        }
    }

    private async Task TryMarkLovedTrackFailed(int trackId)
    {
        try
        {
            await _repository.MarkLovedTrackFailed(trackId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to mark loved track attempt as failed (Track ID {1})", trackId);
        }
    }
}
