namespace Cadenza.Web.Core.Player;

internal class CorePlayer : IPlayer
{
    private readonly List<ISourcePlayer> _sourcePlayers;
    private readonly IState<CurrentTrackState> _currentTrackState;
    private readonly IState<PlaylistState> _playlistState;

    public CorePlayer(IEnumerable<ISourcePlayer> sourcePlayers, IState<CurrentTrackState> currentTrackState, IState<PlaylistState> playlistState)
    {
        _sourcePlayers = sourcePlayers.ToList();
        _currentTrackState = currentTrackState;
        _playlistState = playlistState;
    }

    public async Task Play()
    {
        var service = GetSourcePlayer();
        var currentTrack = _currentTrackState.Value.Track;
        var playlist = _playlistState.Value.Name;
        await service.Play(currentTrack.IdFromSource, currentTrack.Track.Title, currentTrack.Track.ArtistName, playlist, "");
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