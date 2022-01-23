namespace Cadenza.Core;

public class TrackingPlayer : IPlayer
{
    private readonly IPlayer _player;
    private readonly IPlayTracker _tracker;

    private BasicTrack _currentTrack;

    public TrackingPlayer(IPlayer player, IPlayTracker tracker)
    {
        _player = player;
        _tracker = tracker;
    }

    public async Task<TrackProgress> Play(BasicTrack track)
    {
        var progress = await _player.Play(track);
        UpdateTrackDetails(track);
        await UpdateNowPlaying(progress);
        return progress;
    }

    public async Task<TrackProgress> Pause()
    {
        var secondsPlayed = await _player.Pause();
        await UpdateNowPlaying(null);
        return secondsPlayed;
    }

    public async Task<TrackProgress> Resume()
    {
        var progress = await _player.Resume();
        await UpdateNowPlaying(progress);
        return progress;
    }

    public async Task<TrackProgress> Stop()
    {
        var progress = await _player.Stop();
        await RecordPlay(progress);
        return progress;
    }

    private void UpdateTrackDetails(BasicTrack playlistTrack)
    {
        _currentTrack = playlistTrack;
    }

    private async Task RecordPlay(TrackProgress progress)
    {
        if (_currentTrack == null)
            return;

        if (_currentTrack.Source == LibrarySource.Spotify)
            return; // Need a better way to do this - source profile

        var percentagePlayed = GetPercentagePlayed(progress.SecondsPlayed, progress.TotalSeconds);

        if (progress.SecondsPlayed < 4 * 60 && percentagePlayed < 50)
            return;

      //  await _tracker.RecordPlay(_currentTrack, DateTime.Now);
    }

    private async Task UpdateNowPlaying(TrackProgress progress)
    {
        if (_currentTrack == null)
            return;

        if (_currentTrack.Source == LibrarySource.Spotify)
            return; // Need a better way to do this - source profile

        var secondsRemaining = GetSecondsRemaining(progress);
     //   await _tracker.UpdateNowPlaying(_currentTrack, secondsRemaining);
    }

    private int GetSecondsRemaining(TrackProgress progress)
    {
        return progress.TotalSeconds - progress.SecondsPlayed;
    }

    private double GetPercentagePlayed(int secondsPlayed, int duration)
    {
        return duration == 0
            ? 0
            : 100 * secondsPlayed / duration;
    }
}