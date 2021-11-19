namespace Cadenza.Player;

public class AppService : IAppConsumer, IAppController
{
    private readonly IPlayer _player;
    private readonly IStoreSetter _storeSetter;
    private readonly ITrackFinishedConsumer _trackFinishedConsumer;

    public AppService(IStoreSetter storeSetter, IPlayer player, ITrackFinishedConsumer trackFinishedConsumer)
    {
        _player = player;
        _storeSetter = storeSetter;
        _trackFinishedConsumer = trackFinishedConsumer;
    }

    public event TrackEventHandler TrackStarted;
    public event TrackEventHandler TrackPaused;
    public event TrackEventHandler TrackResumed;
    public event TrackEventHandler TrackFinished;
    public event PlaylistEventHandler PlaylistUpdated;

    public IPlaylist CurrentPlaylist { get; private set; }

    public async Task Initialise()
    {
        await _storeSetter.SetValue(StoreKey.Libraries, LibrarySource.Local);
        await _storeSetter.SetValue(StoreKey.CurrentTrackId, null);

        _trackFinishedConsumer.TrackFinished += OnTrackFinished;
    }

    private async Task OnTrackFinished(object sender, TrackFinishedEventArgs args)
    {
        TrackFinished?.Invoke(this, GetArgs());
        CurrentPlaylist.MoveNext();
        await PlayTrack();
    }

    public async Task Play(PlaylistDefinition playlistDefinition)
    {
        CurrentPlaylist = new Playlist(playlistDefinition);
        await PlayTrack();
        PlaylistUpdated?.Invoke(this, new PlaylistEventArgs
        {
            PlaylistName = CurrentPlaylist.Name,
            PlaylistType = CurrentPlaylist.Type
        });
    }

    private async Task PlayTrack()
    {
        await _player.Stop();

        if (CurrentPlaylist.Current != null)
        {
            await _player.Play(CurrentPlaylist.Current);
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

    public async Task SkipNext()
    {
        CurrentPlaylist.MoveNext();
        await PlayTrack();
    }

    public async Task SkipPrevious()
    {
        CurrentPlaylist.MovePrevious();
        await PlayTrack();
    }

    public async Task UpdateSources(List<LibrarySource> enabledSources)
    {
        await _storeSetter.SetValues(StoreKey.Libraries, enabledSources);
    }

    private TrackEventArgs GetArgs(int? secondsPlayed = null)
    {
        var totalSeconds = CurrentPlaylist.Current.Model.DurationSeconds;
        var played = secondsPlayed ?? totalSeconds;

        return new TrackEventArgs
        {
            CurrentTrack = CurrentPlaylist.Current,
            IsLastTrack = CurrentPlaylist.CurrentIsLast,
            PercentagePlayed = totalSeconds == 0
                ? 0
                : played / totalSeconds
        };
    }
}