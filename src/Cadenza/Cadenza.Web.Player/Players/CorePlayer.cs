using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.Web.Player.Players;

internal class CorePlayer : IPlayer
{
    private readonly List<ISourcePlayer> _sourcePlayers;
    private readonly IState<CurrentTrackState> _currentTrackState;

    public CorePlayer(IEnumerable<ISourcePlayer> sourcePlayers, IState<CurrentTrackState> currentTrackState)
    {
        _sourcePlayers = sourcePlayers.ToList();
        _currentTrackState = currentTrackState;
    }

    public async Task Play()
    {
        var service = GetSourcePlayer();
        await service.Play(_currentTrackState.Value.Track.IdFromSource);
    }

    public async Task<int> Pause()
    {
        var service = GetSourcePlayer();
        var progress = await service.Pause();
        return progress.SecondsPlayed;
    }

    public async Task<int> Resume()
    {
        var service = GetSourcePlayer();
        var progress = await service.Resume();
        return progress.SecondsPlayed;
    }

    public async Task<int> Stop()
    {
        var service = GetSourcePlayer();

        if (service == null)
            return 0;

        var progress = await service.Stop();
        return progress.SecondsPlayed;
    }

    private ISourcePlayer GetSourcePlayer()
    {
        var source = _currentTrackState.Value.Track?.Source;

        if (source == null)
            return null;

        return _sourcePlayers.Single(p => p.Source == source.Value);
    }
}