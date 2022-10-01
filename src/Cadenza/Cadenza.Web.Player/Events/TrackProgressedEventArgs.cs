namespace Cadenza.Web.Player.Events;

internal delegate void TrackProgressedEventHandler(object sender, TrackProgressedEventArgs e);

internal class TrackProgressedEventArgs : EventArgs
{
    public TrackProgressedEventArgs(int totalSeconds, int secondsPlayed)
    {
        ProgressPercentage = totalSeconds == 0
            ? 0
            : 100 * (double)secondsPlayed / totalSeconds;
    }

    public double ProgressPercentage { get; set; }
}