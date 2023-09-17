using Cadenza.State.Store;
using Fluxor;
using System.Text.Json;

namespace Cadenza.Web.Player.Players;

internal class CorePlayer : IPlayer
{
    private readonly IState<PlaylistTrackState> _playlistTrackState;
    private readonly List<ISourcePlayer> _sourcePlayers;
    private readonly List<IUtilityPlayer> _utilityPlayers;

    public CorePlayer(IState<PlaylistTrackState> playlistTrackState, IEnumerable<ISourcePlayer> sourcePlayers, IEnumerable<IUtilityPlayer> utilityPlayers)
    {
        _playlistTrackState = playlistTrackState;
        _sourcePlayers = sourcePlayers.ToList();
        _utilityPlayers = utilityPlayers.ToList();
    }

    public async Task Play(PlayTrack track)
    {
        var service = GetCurrentSourcePlayer(track.Source);
        await service.Play(track.IdFromSource);

        var duration = track.Duration;
        var progress = new TrackProgress(0, duration);

        await RunUtilities(p => p.OnPlay(progress));
    }

    public async Task Pause()
    {
        var service = GetCurrentSourcePlayer();
        var progress = await service.Pause();
        await RunUtilities(p => p.OnPause(progress));
    }

    public async Task Resume()
    {
        var service = GetCurrentSourcePlayer();
        var progress = await service.Resume();
        await RunUtilities(p => p.OnResume(progress));
    }

    public async Task Stop()
    {
        var service = GetCurrentSourcePlayer();

        if (service == null)
            return;

        var progress = await service.Stop();
        if (progress.TotalSeconds == -1)
        {
            var duration = _playlistTrackState.Value.CurrentTrack.Duration;
            progress = new TrackProgress(duration, duration);
        }

        await RunUtilities(p => p.OnStop(progress));
    }

    private ISourcePlayer GetCurrentSourcePlayer(LibrarySource? source = null)
    {
        var currentSource = source ?? _playlistTrackState.Value.CurrentTrack?.Source;

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