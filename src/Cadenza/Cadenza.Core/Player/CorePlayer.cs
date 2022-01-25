namespace Cadenza.Core;

public class CorePlayer : IPlayer
{
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly List<ISourcePlayer> _sourcePlayers;
    private readonly List<IUtilityPlayer> _utilityPlayers;
    private readonly ITrackRepository _trackRepository;

    public CorePlayer(IStoreGetter store, IEnumerable<ISourcePlayer> sourcePlayers, IStoreSetter storeSetter, IEnumerable<IUtilityPlayer> utilityPlayers, ITrackRepository trackRepository)
    {
        _storeGetter = store;
        _storeSetter = storeSetter;
        _sourcePlayers = sourcePlayers.ToList();
        _utilityPlayers = utilityPlayers.ToList();
        _trackRepository = trackRepository;
    }

    public async Task<TrackProgress> Play(BasicTrack track)
    {
        var service = await GetCurrentService(track.Source);
        await service.Play(track.Id);
        var summary = await _trackRepository.GetSummary(track.Source, track.Id);
        var progress = new TrackProgress(0, summary.DurationSeconds);
        await _storeSetter.SetValue(StoreKey.CurrentTrack, summary);
        await _storeSetter.SetValue(StoreKey.CurrentTrackSource, track.Source);
        await RunUtilities(p => p.OnPlay(progress));
        return progress;
    }

    public async Task<TrackProgress> Pause()
    {
        var service = await GetCurrentService();
        var progress = await service.Pause();
        await RunUtilities(p => p.OnPause(progress));
        return progress;
    }

    public async Task Stop()
    {
        var service = await GetCurrentService();
        if (service == null)
            return;

        var progress = await service.Stop();

        //if (progress.SecondsPlayed == -1 && progress.TotalSeconds == -1)
        //{
        //    // track finished playing but duration data wasn't available
        //    var summary = await _storeGetter.GetValue<TrackSummary>(StoreKey.CurrentTrack);
        //    progress = new TrackProgress(summary.DurationSeconds, summary.DurationSeconds);
        //}

        await RunUtilities(p => p.OnStop(progress));
        await _storeSetter.SetValue(StoreKey.CurrentTrack, null);
        await _storeSetter.SetValue(StoreKey.CurrentTrackSource, null);
    }

    public async Task<TrackProgress> Resume()
    {
        var service = await GetCurrentService();
        var progress = await service.Resume();
        await RunUtilities(p => p.OnResume(progress));
        return progress;
    }

    private async Task<IAudioPlayer> GetCurrentService(LibrarySource? source = null)
    {
        if (!source.HasValue)
        {
            var storedSource = await _storeGetter.GetString(StoreKey.CurrentTrackSource);
            if (storedSource == null)
                return null;

            if (!Enum.TryParse<LibrarySource>(storedSource, out LibrarySource result))
                return null;

            source = result;
        }

        return _sourcePlayers.Single(p => p.Source == source.Value);
    }

    private async Task RunUtilities(Func<IUtilityPlayer, Task> action)
    {
        foreach (var player in _utilityPlayers)
        {
            await action(player);
        }
    }
}