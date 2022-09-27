using Cadenza.Domain.Model;

namespace Cadenza.Web.Core.CurrentlyPlaying;

public delegate Task TrackEventHandler(object sender, TrackEventArgs e);

public class TrackEventArgs : EventArgs
{
    public PlayTrack CurrentTrack { get; set; }
    public int PercentagePlayed { get; set; }
    public bool IsLastTrack { get; set; }
    public string Error { get; set; }
}
