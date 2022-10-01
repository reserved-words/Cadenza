namespace Cadenza.Web.Core.Players;

internal class CorePlayer : IPlayer
{
    private readonly ICurrentTrackStore _store;
    private readonly List<ISourcePlayer> _sourcePlayers;
    private readonly List<IUtilityPlayer> _utilityPlayers;
    private readonly ITrackRepository _trackRepository;

    public CorePlayer(ICurrentTrackStore store, IEnumerable<ISourcePlayer> sourcePlayers, IEnumerable<IUtilityPlayer> utilityPlayers, ITrackRepository trackRepository)
    {
        _store = store;
        _sourcePlayers = sourcePlayers.ToList();
        _utilityPlayers = utilityPlayers.ToList();
        _trackRepository = trackRepository;
    }

    public async Task<TrackProgress> Play(PlayTrack track)
    {
        var service = await GetCurrentSourcePlayer(track.Source);
        await service.Play(track.Id);

        var fullTrack = await _trackRepository.GetTrack(track.Id);
        var progress = new TrackProgress(0, fullTrack.Track.DurationSeconds);

        await StoreCurrentTrack(fullTrack);
        await RunUtilities(p => p.OnPlay(progress));

        return progress;
    }

    public async Task<TrackProgress> Pause()
    {
        var service = await GetCurrentSourcePlayer();
        var progress = await service.Pause();
        await RunUtilities(p => p.OnPause(progress));
        return progress;
    }

    public async Task<TrackProgress> Resume()
    {
        var service = await GetCurrentSourcePlayer();
        var progress = await service.Resume();
        await RunUtilities(p => p.OnResume(progress));
        return progress;
    }

    public async Task Stop()
    {
        var service = await GetCurrentSourcePlayer();

        if (service == null)
            return;

        var progress = await service.Stop();
        if (progress.TotalSeconds == -1)
        {
            var track = await _store.GetCurrentTrack();
            progress = new TrackProgress(track.Track.DurationSeconds, track.Track.DurationSeconds);
        }

        await RunUtilities(p => p.OnStop(progress));
        await StoreCurrentTrack(null);
    }

    private async Task<ISourcePlayer> GetCurrentSourcePlayer(LibrarySource? source = null)
    {
        var currentSource = source ?? await GetCurrentSource();

        if (!currentSource.HasValue)
            return null;

        return _sourcePlayers.Single(p => p.Source == currentSource.Value);
    }

    private async Task<LibrarySource?> GetCurrentSource()
    {
        return await _store.GetCurrentSource();
    }

    private async Task RunUtilities(Func<IUtilityPlayer, Task> action)
    {
        foreach (var player in _utilityPlayers)
        {
            await action(player);
        }
    }

    private async Task StoreCurrentTrack(TrackFull track)
    {
        await _store.StoreCurrentTrack(track);
    }
}