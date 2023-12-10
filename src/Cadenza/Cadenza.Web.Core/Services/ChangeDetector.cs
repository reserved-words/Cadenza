using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Core.Services;

internal class ChangeDetector : IChangeDetector
{
    public bool HasArtistChanged(ArtistDetailsVM originalArtist, ArtistDetailsVM editedArtist)
    {
        return editedArtist.Name != originalArtist.Name
            || editedArtist.Grouping.Name != originalArtist.Grouping.Name
            || editedArtist.Genre != originalArtist.Genre
            || editedArtist.City != originalArtist.City
            || editedArtist.State != originalArtist.State
            || editedArtist.Country != originalArtist.Country
            || editedArtist.ImageBase64 != originalArtist.ImageBase64
            || HaveTagsChanged(originalArtist.Tags, editedArtist.Tags); 
    }

    public bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM editedTrack)
    {
        return editedTrack.Title != originalTrack.Title
             || editedTrack.Year != originalTrack.Year
             || editedTrack.Lyrics != originalTrack.Lyrics
             || HaveTagsChanged(editedTrack.Tags, originalTrack.Tags);
    }

    private bool HaveTagsChanged(IReadOnlyCollection<string> originalTags, IReadOnlyCollection<string> newTags)
    {
        if (originalTags.Count != newTags.Count)
            return true;

        if (originalTags.Except(newTags).Any())
            return true;

        return false;
    }
}
