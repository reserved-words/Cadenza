namespace Cadenza.Web.Common.Interfaces;

public interface IChangeDetector
{
    bool HasArtistChanged(ArtistDetailsVM originalArtist, ArtistDetailsVM editedArtist);
    bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM editedTrack);
}