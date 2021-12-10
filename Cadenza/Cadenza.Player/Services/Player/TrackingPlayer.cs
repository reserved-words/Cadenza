namespace Cadenza.Player;

public class TrackingPlayer : IPlayer
{
    private readonly IPlayer _player;
    private readonly IPlayTracker _tracker;
    private readonly IViewModelLibrary _library;

    private PlayingTrack _currentTrack;

    public TrackingPlayer(IPlayer player, IPlayTracker tracker, IViewModelLibrary library)
    {
        _player = player;
        _tracker = tracker;
        _library = library;
    }

    public async Task Play(PlayingTrack playlistTrack)
    {
        await _player.Play(playlistTrack);
        await UpdateTrackDetails(playlistTrack);
        await UpdateNowPlaying(0);
    }

    public async Task<int> Pause()
    {
        var secondsPlayed = await _player.Pause();
        await UpdateNowPlaying(null);
        return secondsPlayed;
    }

    public async Task<int> Resume()
    {
        var secondsPlayed = await _player.Resume();
        await UpdateNowPlaying(secondsPlayed);
        return secondsPlayed;
    }

    public async Task<int> Stop()
    {
        var secondsPlayed = await _player.Stop();
        await RecordPlay(secondsPlayed);
        return secondsPlayed;
    }

    private async Task UpdateTrackDetails(PlayingTrack playlistTrack)
    {
        _currentTrack = playlistTrack;
    }

    private async Task RecordPlay(int secondsPlayed)
    {
        if (_currentTrack == null)
            return;

        if (_currentTrack.Source == LibrarySource.Spotify)
            return; // Need a better way to do this - source profile

        var percentagePlayed = GetPercentagePlayed(secondsPlayed, _currentTrack.DurationSeconds);

        if (secondsPlayed < 4 * 60 && percentagePlayed < 50)
            return;

        await _tracker.RecordPlay(_currentTrack, DateTime.Now);
    }

    private async Task UpdateNowPlaying(int? secondsPlayed = null)
    {
        if (_currentTrack == null)
            return;

        if (_currentTrack.Source == LibrarySource.Spotify)
            return; // Need a better way to do this - source profile

        var secondsRemaining = GetSecondsRemaining(secondsPlayed);
        await _tracker.UpdateNowPlaying(_currentTrack, secondsRemaining);
    }

    private int GetSecondsRemaining(int? secondsPlayed = null)
    {
        if (!secondsPlayed.HasValue)
            return 1;

        var totalSeconds = _currentTrack.DurationSeconds;

        return totalSeconds - secondsPlayed.Value;
    }

    private double GetPercentagePlayed(int secondsPlayed, int duration)
    {
        return duration == 0
            ? 0
            : 100 * secondsPlayed / duration;
    }
}