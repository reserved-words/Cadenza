namespace Cadenza.Web.Core.Services;

internal class AppService : IAppConsumer, IAppController
{
    private readonly IPlayer _player;

    public AppService(IPlayer player, ITrackFinishedConsumer trackFinishedConsumer)
    {
        _player = player;

        trackFinishedConsumer.TrackFinished += OnTrackFinished;
    }

    public event TrackEventHandler TrackFinished;
    public event TrackEventHandler TrackPaused;
    public event TrackEventHandler TrackResumed;
    public event TrackEventHandler TrackStarted;

    public event PlaylistEventHandler PlaylistFinished;
    public event PlaylistEventHandler PlaylistLoading;
    public event PlaylistEventHandler PlaylistStarted;

    public event ItemEventHandler ItemRequested;

    private IPlaylist _currentPlaylist;

    private async Task OnTrackFinished(object sender, TrackFinishedEventArgs args)
    {
        await SkipNext();
    }

    public async Task Play(PlaylistDefinition playlistDefinition)
    {
        _currentPlaylist = new Playlist(playlistDefinition);
        await PlayTrack();
        await PlaylistStarted?.Invoke(this, GetPlaylistArgs());
    }

    private async Task PlayTrack()
    {
        await _player.Stop();

        if (_currentPlaylist.Current == null)
            return;

        var progress = await _player.Play(_currentPlaylist.Current);
        await TrackStarted?.Invoke(this, GetProgressArgs(progress));
    }

    public async Task Pause()
    {
        var progress = await _player.Pause();
        await TrackPaused?.Invoke(this, GetProgressArgs(progress));
    }

    public async Task Resume()
    {
        var progress = await _player.Resume();
        await TrackResumed?.Invoke(this, GetProgressArgs(progress));
    }

    public async Task SkipNext()
    {
        await TrackFinished?.Invoke(this, GetFinishedArgs());
        if (_currentPlaylist.CurrentIsLast)
        {
            await StopPlaylist();
            return;
        }

        await _currentPlaylist.MoveNext();
        await PlayTrack();
    }

    public async Task SkipPrevious()
    {
        await TrackFinished?.Invoke(this, GetFinishedArgs());
        await _currentPlaylist.MovePrevious();
        await PlayTrack();
    }

    private TrackEventArgs GetFinishedArgs()
    {
        return new TrackEventArgs
        {
            CurrentTrack = _currentPlaylist.Current,
            IsLastTrack = _currentPlaylist.CurrentIsLast,
            PercentagePlayed = 100
        };
    }

    private PlaylistEventArgs GetPlaylistArgs(string error = null)
    {
        return new PlaylistEventArgs
        {
            Playlist = _currentPlaylist.Id,
            Error = error
        };
    }

    private TrackEventArgs GetProgressArgs(TrackProgress progress)
    {
        return new TrackEventArgs
        {
            CurrentTrack = _currentPlaylist.Current,
            IsLastTrack = _currentPlaylist.CurrentIsLast,
            PercentagePlayed = progress.TotalSeconds == 0
                ? 0
                : progress.SecondsPlayed / progress.TotalSeconds
        };
    }

    public async Task LoadingPlaylist()
    {
        await StopPlaylist();
        await PlaylistLoading?.Invoke(this, new PlaylistEventArgs());
    }

    private async Task StopPlaylist()
    {
        await _player.Stop();

        if (_currentPlaylist != null)
        {
            await PlaylistFinished?.Invoke(this, GetPlaylistArgs());
            _currentPlaylist = null;
        }
    }

    public async Task View(ViewItem item)
    {
        await ItemRequested?.Invoke(this, new ItemEventArgs { Item = item });
    }
}