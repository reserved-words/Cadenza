namespace Cadenza.Core.Player;

public delegate void TrackProgressedEventHandler(object sender, TrackProgressedEventArgs e);

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