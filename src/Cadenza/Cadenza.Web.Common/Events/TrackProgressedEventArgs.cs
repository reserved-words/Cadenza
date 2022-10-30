namespace Cadenza.Web.Common.Events;

public class TrackProgressedEventArgs : EventArgs
{
    public TrackProgressedEventArgs(int totalSeconds, int secondsPlayed)
    {
        SecondsPlayed = secondsPlayed;
        TotalSeconds = totalSeconds;
        ProgressPercentage = totalSeconds == 0
            ? 0
            : 100 * (double)secondsPlayed / totalSeconds;
    }

    public double ProgressPercentage { get; set; }
    public int SecondsPlayed { get; set; }
    public int TotalSeconds { get; set; }
}