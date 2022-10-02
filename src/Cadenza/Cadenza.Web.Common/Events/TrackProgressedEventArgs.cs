namespace Cadenza.Web.Common.Events;

public class TrackProgressedEventArgs : EventArgs
{
    public TrackProgressedEventArgs(int totalSeconds, int secondsPlayed)
    {
        ProgressPercentage = totalSeconds == 0
            ? 0
            : 100 * (double)secondsPlayed / totalSeconds;
    }

    public double ProgressPercentage { get; set; }
}