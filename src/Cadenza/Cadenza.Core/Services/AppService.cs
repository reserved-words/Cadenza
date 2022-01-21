namespace Cadenza.Core;

public class AppService : IAppConsumer, IAppController
{
    private readonly IPlayer _player;
    private readonly ITrackFinishedConsumer _trackFinishedConsumer;
    private readonly ITrackRepository _trackRepository;

    public AppService(IPlayer player, ITrackFinishedConsumer trackFinishedConsumer, ITrackRepository trackRepository)
    {
        _player = player;
        _trackFinishedConsumer = trackFinishedConsumer;
        _trackRepository = trackRepository;
    }

    public event TrackEventHandler TrackStarted;
    public event TrackEventHandler TrackPaused;
    public event TrackEventHandler TrackResumed;
    public event TrackEventHandler TrackFinished;
    public event PlaylistEventHandler PlaylistUpdated;
    public event LibraryEventHandler LibraryUpdated;

    private IPlaylist _currentPlaylist;
    private TrackSummary _TrackSummary;

    public void Initialise()
    {
        _trackFinishedConsumer.TrackFinished += OnTrackFinished;

        LibraryUpdated?.Invoke(this, new LibraryEventArgs());
    }

    private async Task OnTrackFinished(object sender, TrackFinishedEventArgs args)
    {
        TrackFinished?.Invoke(this, GetArgs());
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
            _TrackSummary = await _trackRepository.GetSummary(_currentPlaylist.Current.Source, _currentPlaylist.Current.Id);
            await _player.Play(_TrackSummary);
            TrackStarted?.Invoke(this, GetArgs(0));
        }
    }

    public async Task Pause()
    {
        var secondsPlayed = await _player.Pause();
        TrackPaused?.Invoke(this, GetArgs(secondsPlayed));
    }

    public async Task Resume()
    {
        var secondsPlayed = await _player.Resume();
        TrackResumed?.Invoke(this, GetArgs(secondsPlayed));
    }

    public async Task Stop()
    {
        await _player.Stop();
        _TrackSummary = null;

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

    private TrackEventArgs GetArgs(int? secondsPlayed = null)
    {
        var totalSeconds = _TrackSummary.DurationSeconds;
        var played = secondsPlayed ?? totalSeconds;

        return new TrackEventArgs
        {
            CurrentTrack = _TrackSummary,
            IsLastTrack = _currentPlaylist.CurrentIsLast,
            PercentagePlayed = totalSeconds == 0
                ? 0
                : played / totalSeconds
        };
    }
}