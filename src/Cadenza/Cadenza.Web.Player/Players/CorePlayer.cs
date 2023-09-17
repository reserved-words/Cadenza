using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.Web.Player.Players;

internal class CorePlayer : IPlayer
{
    private readonly IState<CurrentTrackState> _currentTrackState;
    private readonly List<ISourcePlayer> _sourcePlayers;
    private readonly List<IUtilityPlayer> _utilityPlayers;

    public CorePlayer(IState<CurrentTrackState> currentTrackState, IEnumerable<ISourcePlayer> sourcePlayers, IEnumerable<IUtilityPlayer> utilityPlayers)
    {
        _currentTrackState = currentTrackState;
        _sourcePlayers = sourcePlayers.ToList();
        _utilityPlayers = utilityPlayers.ToList();
    }

    public async Task Play(Track track)
    {
        var service = GetCurrentSourcePlayer(track.Source);
        await service.Play(track.IdFromSource);

        var duration = track.DurationSeconds;
        var progress = new TrackProgress(0, duration);

        await RunUtilities(p => p.OnPlay(progress));
    }

    public async Task<int> Pause()
    {
        var service = GetCurrentSourcePlayer();
        var progress = await service.Pause();
        await RunUtilities(p => p.OnPause(progress));
        return progress.SecondsPlayed;
    }

    public async Task<int> Resume()
    {
        var service = GetCurrentSourcePlayer();
        var progress = await service.Resume();
        await RunUtilities(p => p.OnResume(progress));
        return progress.SecondsPlayed;
    }

    public async Task<int> Stop()
    {
        var service = GetCurrentSourcePlayer();

        if (service == null)
            return 0;

        var progress = await service.Stop();
        if (progress.TotalSeconds == -1)
        {
            var duration = _currentTrackState.Value.FullTrack.Track.DurationSeconds;
            progress = new TrackProgress(duration, duration);
        }

        await RunUtilities(p => p.OnStop(progress));
        return progress.SecondsPlayed;
    }

    private ISourcePlayer GetCurrentSourcePlayer(LibrarySource? source = null)
    {
        var currentSource = source ?? _currentTrackState.Value.FullTrack?.Track.Source;

        if (currentSource == null)
            return null;

        return _sourcePlayers.Single(p => p.Source == currentSource.Value);
    }

    private async Task RunUtilities(Func<IUtilityPlayer, Task> action)
    {
        foreach (var player in _utilityPlayers)
        {
            await action(player);
        }
    }
}