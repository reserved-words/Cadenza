namespace Cadenza.Common;

public class TrackProgress
{
    public TrackProgress()
    {

    }

    public TrackProgress(int secondsPlayed, int totalSeconds)
    {
        SecondsPlayed = secondsPlayed;
        TotalSeconds = totalSeconds;
    }

    public int SecondsPlayed { get; set; }
    public int TotalSeconds { get; set; }

    public int SecondsRemaining => TotalSeconds - SecondsPlayed;
    public int PercentagePlayed => TotalSeconds == 0 ? 0 : (SecondsPlayed * 100 / TotalSeconds);
}