using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.Web.Player.Players;

internal class CorePlayer : IPlayer
{
    private readonly List<ISourcePlayer> _sourcePlayers;
    private readonly List<IUtilityPlayer> _utilityPlayers;

    public CorePlayer(IEnumerable<ISourcePlayer> sourcePlayers, IEnumerable<IUtilityPlayer> utilityPlayers)
    {
        _sourcePlayers = sourcePlayers.ToList();
        _utilityPlayers = utilityPlayers.ToList();
    }

    public async Task Play(Track track)
    {
        var service = GetSourcePlayer(track);
        await service.Play(track.IdFromSource);

        var duration = track.DurationSeconds;
        var progress = new TrackProgress(0, duration);

        await RunUtilities(p => p.OnPlay(progress));
    }

    public async Task<int> Pause(Track track)
    {
        var service = GetSourcePlayer(track);
        var progress = await service.Pause();
        await RunUtilities(p => p.OnPause(progress));
        return progress.SecondsPlayed;
    }

    public async Task<int> Resume(Track track)
    {
        var service = GetSourcePlayer(track);
        var progress = await service.Resume();
        await RunUtilities(p => p.OnResume(progress));
        return progress.SecondsPlayed;
    }

    public async Task<int> Stop(Track track)
    {
        var service = GetSourcePlayer(track);

        if (service == null)
            return 0;

        var progress = await service.Stop();
        if (progress.TotalSeconds == -1)
        {
            var duration = track.DurationSeconds;
            progress = new TrackProgress(duration, duration);
        }

        await RunUtilities(p => p.OnStop(progress));
        return progress.SecondsPlayed;
    }

    private ISourcePlayer GetSourcePlayer(Track track)
    {
        var source =track?.Source;

        if (source == null)
            return null;

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