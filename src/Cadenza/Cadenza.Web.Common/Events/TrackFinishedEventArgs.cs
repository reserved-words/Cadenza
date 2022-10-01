namespace Cadenza.Web.Common.Events;

public delegate Task TrackFinishedEventHandler(object sender, TrackFinishedEventArgs e);

public class TrackFinishedEventArgs : EventArgs
{
}