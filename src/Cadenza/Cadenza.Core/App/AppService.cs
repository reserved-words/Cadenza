namespace Cadenza.Core;

public class AppService : IAppConsumer, IAppController
{
    private readonly IPlayer _player;
    private readonly ITrackFinishedConsumer _trackFinishedConsumer;

    public AppService(IPlayer player, ITrackFinishedConsumer trackFinishedConsumer)
    {
        _player = player;
        _trackFinishedConsumer = trackFinishedConsumer;

        _trackFinishedConsumer.TrackFinished += OnTrackFinished;
    }

    public event TrackEventHandler TrackStarted;
    public event TrackEventHandler TrackPaused;
    public event TrackEventHandler TrackResumed;
    public event TrackEventHandler TrackFinished;
    public event PlaylistEventHandler PlaylistUpdated;
    public event LibraryEventHandler LibraryUpdated;

    private IPlaylist _currentPlaylist;

    public void Initialise()
    {
        LibraryUpdated?.Invoke(this, new LibraryEventArgs());
    }

    private async Task OnTrackFinished(object sender, TrackFinishedEventArgs args)
    {
        TrackFinished?.Invoke(this, GetFinishedArgs());
        await _currentPlaylist.MoveNext();
        await PlayTrack();
    }

    public async Task Play(PlaylistDefinition playlistDefinition)
    {
        _currentPlaylist = new Playlist(playlistDefinition);
        await PlayTrack();
        PlaylistUpdated?.Invoke(this, new PlaylistEventArgs
        {
            PlaylistName = _currentPlaylist.Name,
            PlaylistType = _currentPlaylist.Type
        });
    }

    private async Task PlayTrack()
    {
        await Stop();

        if (_currentPlaylist.Current != null)
        {
            var progress = await _player.Play(_currentPlaylist.Current);
            TrackStarted?.Invoke(this, GetProgressArgs(progress));
        }
    }

    public async Task Pause()
    {
        var progress = await _player.Pause();
        TrackPaused?.Invoke(this, GetProgressArgs(progress));
    }

    public async Task Resume()
    {
        var progress = await _player.Resume();
        TrackResumed?.Invoke(this, GetProgressArgs(progress));
    }

    public async Task Stop()
    {
        await _player.Stop();

    }

    public async Task SkipNext()
    {
        await _currentPlaylist.MoveNext();
        await PlayTrack();
    }

    public async Task SkipPrevious()
    {
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
}