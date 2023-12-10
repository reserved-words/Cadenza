namespace Cadenza.Web.Common.Interfaces;

public interface IChangeDetector
{
    bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM editedTrack);
}